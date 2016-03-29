using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Network_Controller;
using System.Net.Sockets;

// Created by Kabir Sandhu and Isabelle Chalhoub
// Date Modified: Nov 17, 2015
namespace View
{
    /// <summary>
    /// Our gui for AgCubio
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Callback function to be invoked in network controller
        /// </summary>
        /// <param name="state"></param>
        private delegate void callback(StateObject state);

        /// <summary>
        /// World object used to keep track of all cubes
        /// </summary>
        private World world;

        /// <summary>
        /// Name of player 
        /// </summary>
        private string playerName;

        /// <summary>
        /// Unique Id of player cube
        /// </summary>
        private int playerId;

        /// <summary>
        /// Brush used to paint cubes
        /// </summary>
        private System.Drawing.SolidBrush myBrush;

        /// <summary>
        /// Font used in form
        /// </summary>
        private Font font;

        /// <summary>
        /// Socket of connection
        /// </summary>
        private Socket socket;

        /// <summary>
        /// Boolean to keep track whether or not the game has been started
        /// </summary>
        private bool gameStarted;

        /// <summary>
        /// Counter to track FPS
        /// </summary>
        private int FpsCounter;

        /// <summary>
        /// Used to store incomplete json strings
        /// </summary>
        private string incompleteJson;

        /// <summary>
        /// Stopwatch used to track FPS
        /// </summary>
        private System.Diagnostics.Stopwatch timer;

        /// <summary>
        /// Keeps track of total width of split cubes
        /// </summary>
        private int totalWidth;

        /// <summary>
        /// Initialize the form
        /// </summary>
        public Form1()
        {
            incompleteJson = "";
            font = new Font("Adobe Gothic Std", 14);
            gameStarted = false;
            timer = new System.Diagnostics.Stopwatch();
            world = new World();
            InitializeComponent();
            panel1.Show();
            this.DoubleBuffered = true;
            FpsCounter = 0;
            totalWidth = 0;
        }

        /// <summary>
        /// On the button click - try to connect to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            world = new World();
            errorBox.Text = "";

            // Save the name they typed in
            playerName = nameBox.Text;

            try
            {
                // Connect_to_Server
                socket = Network_Controller.Network_Controller.Connect_to_Server((callback)connect, ipBox.Text);
            }
            catch (Exception err)
            {
                // If there is a problem connecting to the server, display a message and wait
                errorBox.Text = err.Message;
                return;
            }
        }

        /// <summary>
        /// Paint method that updates the screen with all the cube movements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.Clear(Color.FromArgb(30, 30, 30));

            //Lock world so it doesn't change while updating the GUI
            lock (world)
            {
                // If your cube's mass = 0, you died. Display message and close the window
                if ((!world.cubes.ContainsKey(playerId)) && gameStarted)
                {
                    MessageBox.Show("you died!\n" +
                        "\nStats: " +
                        "\n Final Mass: " + massBox.Text +
                       "\n Final Width: " + widthBox.Text);
                    this.Close();
                    return;
                }

                // Get player cube
                Cube playerCube = world.cubes[playerId];
                int playerWidth = playerCube.width;

                //Scaling factor to scale game view
                int scalingFactor;

                //If the cube is split, use totalWidth, otherwise use player width
                if (totalWidth > 0)
                {
                    scalingFactor = 1000 / (totalWidth * 2);
                    widthBox.Text = "" + totalWidth;
                }
                else
                {
                    scalingFactor = 1000 / (playerCube.width * 2);
                    widthBox.Text = "" + playerWidth;
                }

                //Get player coordinates
                int xPlayer = (int)playerCube.loc_x * scalingFactor;
                int yPlayer = (int)playerCube.loc_y * scalingFactor;


                //Total mass to keep track of mass of all split cubes
                int totalMass = 0;

                //set food label
                foodBox.Text = "" + world.cubes.Count;

                // Paint each cube from the world dictionary
                foreach (KeyValuePair<int, Cube> item in world.cubes)
                {

                    // Get cube and cube color
                    Cube cube = item.Value;
                    Color color = Color.FromArgb(cube.argb_color);
                    myBrush = new System.Drawing.SolidBrush(color);

                    // Caclculate zoom and draw cube
                    int x = (int)cube.loc_x - (cube.width / 2);
                    int y = (int)cube.loc_y - (cube.width / 2);
                    e.Graphics.FillRectangle(myBrush, new Rectangle(x * scalingFactor - xPlayer + 500, y * scalingFactor - yPlayer + 500, scalingFactor * cube.width, scalingFactor * cube.width));

                    // Measure length of the cube's name
                    SizeF stringSize = new SizeF();
                    stringSize = e.Graphics.MeasureString(cube.Name, font);

                    // If the cube is a player and the cube can fit the name, write its name
                    if (!cube.food && cube.width > stringSize.Width)
                    {
                        myBrush.Color = Color.Gray;
                        e.Graphics.DrawString(cube.Name, font, myBrush, new PointF((int)x * scalingFactor + (cube.width * scalingFactor / 2) - (stringSize.Width / 2) - xPlayer + 500, (int)y * scalingFactor + (cube.width * scalingFactor / 2) - (stringSize.Height / 2) - yPlayer + 500));
                    }

                    if (cube.team_id == playerId)
                        totalMass += (int)cube.Mass;
                }

                //Set Mass label
                if (totalMass > 0)
                {
                    massBox.Text = "" + totalMass;
                }
                else
                    massBox.Text = "" + (int)playerCube.Mass;

                //Set total Width
                totalWidth = (int)Math.Pow(totalMass, 0.65);
            }

            // If the game is started - send mouse coordinates to server
            if (gameStarted)
            {
                double xMouse = base.PointToClient(Control.MousePosition).X;
                double yMouse = base.PointToClient(Control.MousePosition).Y;
                Network_Controller.Network_Controller.Send(socket, "(move, " + xMouse + ", " + yMouse + ")\n");
            }

            // FPS Counter
            if (timer.ElapsedMilliseconds >= 1000)
            {
                System.Diagnostics.Debug.WriteLine("Updating fps " + FpsCounter);
                fpsBox.Text = "" + FpsCounter;
                FpsCounter = 0;
                timer.Reset();
                timer.Start();
            }

            //Increment fps counter
            FpsCounter++;

            //Update text boxes
            Application.DoEvents();

            this.Invalidate();
            Focus();
        }

        /// <summary>
        /// Callback for once we have connected to the server
        /// </summary>
        /// <param name="state"></param>
        private void connect(StateObject state)
        {
            try
            {
                // Send name to server
                Network_Controller.Network_Controller.Send(socket, playerName + "\n");

                // Set callback to getPlayerCube method
                state.callback = (callback)getPlayerCube;
                // Ask for data
                Network_Controller.Network_Controller.i_want_more_data(state);
            }
            catch (Exception)
            {
                //If connection to server is unsuccesful. Display a message indicating the server could not be connected to.
                Invoke((Action)delegate() { errorBox.Text = "Could not connect to server!"; });
            }
        }

        /// <summary>
        /// Callback used to get the player cube from the server
        /// </summary>
        /// <param name="state"></param>
        private void getPlayerCube(StateObject state)
        {
            // Convert byte array to json strings
            string jsonStrings = state.jsonBuilder.Append(Encoding.UTF8.GetString(state.buffer, 0, state.buffer.Length)).ToString();

            // Split the json string into substrings separated by a newline
            List<string> jsonCubes = new List<string>(jsonStrings.Split(new string[] { "\n" }, StringSplitOptions.None));

            //The player is the first json string sent by the server
            playerId = world.firstCube(jsonCubes[0]).uid;

            //Change callback to recieveData
            state.callback = (callback)recieveData;

            //Ask server for more data
            Network_Controller.Network_Controller.i_want_more_data(state);

            //Set game started to true
            gameStarted = true;

            //Start timer for fps counter
            timer.Start();

            //Hide the panel and display game area
            Invoke((Action)delegate() { panel1.Hide(); });
        }

        /// <summary>
        /// Callback for once we have received data
        /// </summary>
        /// <param name="state"></param>
        private void recieveData(StateObject state)
        {
            try
            {
                // Convert byte array to json strings
                string jsonStrings = state.jsonBuilder.Append(Encoding.UTF8.GetString(state.buffer, 0, state.buffer.Length)).ToString();
                // After we make the string - clear string builder
                state.jsonBuilder.Clear();
                state.buffer = new byte[state.buffer_size];

                // Split the json string into substrings separated by a newline
                List<string> jsonCubes = new List<string>(jsonStrings.Split(new string[] { "\n" }, StringSplitOptions.None));

                // Lock while we update the world dictionary
                lock (world)
                {
                    // For each string, deserialize and add to dictionary
                    foreach (string json in jsonCubes)
                    {
                        //If json is valid, update world and set incomplete json to empty string
                        if (json.StartsWith("{") && (json.EndsWith("}")))
                        {
                            world.updateWorld(json);
                            incompleteJson = "";
                        }
                        //Otherwise, if json is zero, skip it
                        else if (json.StartsWith("\0"))
                        {
                            incompleteJson = "";
                            continue;
                        }
                        //Otherwise if json is incomplete, try to complete it or save it to be completed in next recieve
                        else
                        {
                            if (incompleteJson != "")
                            {
                                incompleteJson += json;
                                world.updateWorld(incompleteJson);
                                incompleteJson = "";
                            }
                            else
                                incompleteJson = json;
                        }
                    }
                }

                // Ask for more data!
                if (gameStarted)
                    Network_Controller.Network_Controller.i_want_more_data(state);
            }
            catch (Exception)
            {
                //If connection to server is interrupted, displays a message and closes the game
                Invoke((Action)delegate() { MessageBox.Show("Lost connection to server!"); this.Close(); });
            }
        }

        /// <summary>
        /// Method which sends split command to server when space bar is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == (int)Keys.Space) && gameStarted)
            {
                int x = base.PointToClient(Control.MousePosition).X;
                int y = base.PointToClient(Control.MousePosition).Y;
                Network_Controller.Network_Controller.Send(socket, "(split, " + x + ", " + y + ")\n");
            }
        }
    }
}
