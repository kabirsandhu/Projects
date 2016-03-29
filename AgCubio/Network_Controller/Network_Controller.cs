using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

// Created by Kabir Sandhu and Isabelle Chalhoub
// Date Modified: Nov 17, 2015
namespace Network_Controller
{
    /// <summary>
    /// Our state class
    /// </summary>
    public class StateObject
    {
        /// <summary>
        /// Socket of connection
        /// </summary>
        public Socket socket;

        /// <summary>
        /// Callback function to be stored as a delegate
        /// </summary>
        public Delegate callback { get; set; }

        /// <summary>
        /// Byte buffer
        /// </summary>
        public byte[] buffer { get; set; }

        /// <summary>
        /// Size of byte buffer
        /// </summary>
        public int buffer_size { get; set; }

        /// <summary>
        /// Data string
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Stringbuilder for json
        /// </summary>
        public StringBuilder jsonBuilder { get; set; }

        /// <summary>
        /// Byte array for storing outgoing bytes to be sent
        /// </summary>
        public byte[] outgoing { get; set; }

        /// <summary>
        /// Constructor for state object
        /// </summary>
        public StateObject()
        {
            buffer_size = 10000;
            buffer = new byte[buffer_size];
            jsonBuilder = new StringBuilder();
        }
    }
    /// <summary>
    /// Our Network_Controller - a TCP client
    /// </summary>
    public static class Network_Controller
    {
        private const int port = 11000;

        /// <summary>
        /// Tries to initially connect to the server given a server hostname
        /// </summary>
        /// <param name="callback"> delegate for the function to be called next </param>
        /// <param name="hostname"> server hostname that will be resolved to an IP address </param>
        /// <returns> socket that the client connected to </returns>
        public static Socket Connect_to_Server(Delegate callback, string hostname)
        {
            try
            {
                // Resolve IP address
                IPHostEntry IPHost = Dns.GetHostEntry(hostname);
                IPAddress address = IPHost.AddressList[1];
                IPEndPoint endpoint = new IPEndPoint(address, port);

                // Start new client
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                StateObject state = new StateObject();
                state.callback = callback;
                state.socket = client;

                // Try to connect
                client.BeginConnect(hostname, port, new AsyncCallback(Connected_to_Server), state);
                return client;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connect_to_Server callback that is called when a connection has been made
        /// </summary>
        /// <param name="state_in_an_ar_object"> current state </param>
        public static void Connected_to_Server(IAsyncResult state_in_an_ar_object)
        {
            StateObject state = (StateObject)state_in_an_ar_object.AsyncState;
            try
            {
                //Get socket form state object
                Socket client = state.socket;

                //Complete connection
                client.EndConnect(state_in_an_ar_object);

                //Call delegate in state object
                state.callback.DynamicInvoke(state);
            }
            catch (Exception)
            {
                state.callback.DynamicInvoke(state);
                return;
            }
        }

        /// <summary>
        /// Callback that is called when we have received data - records data received
        /// </summary>
        /// <param name="state_in_an_ar_object"> current state </param>
        public static void ReceiveCallback(IAsyncResult state_in_an_ar_object)
        {
            StateObject state = (StateObject)state_in_an_ar_object.AsyncState;
            try
            {
                Socket client = state.socket;

                // Get length of data received
                int bytes = client.EndReceive(state_in_an_ar_object);

                // Server disconnected
                if (bytes == 0)
                {
                    client.Close();
                }
                // Send data to the view
                else
                {
                    // Invoke the state delegate - go to view
                    state.callback.DynamicInvoke(state);
                }
            }
            catch (Exception)
            {
                state.callback.DynamicInvoke(state);
            }
        }

        /// <summary>
        /// Method that tries to receive more data form server
        /// </summary>
        /// <param name="state"> current state </param>
        public static void i_want_more_data(StateObject state)
        {
            // Receive data
            state.socket.BeginReceive(state.buffer, 0, state.buffer_size, 0, new AsyncCallback(ReceiveCallback), state);
        }

        /// <summary>
        /// Tries to send information to the server
        /// </summary>
        /// <param name="socket"> socket we are connected to </param>
        /// <param name="data"> information to be sent </param>
        public static void Send(Socket socket, String data)
        {
            try
            {
                // Encode string to byte array
                byte[] byteData = Encoding.UTF8.GetBytes(data);

                // Send the data
                socket.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallBack), socket);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Callback that is called after we have tried to send data.
        /// Checks if the data got sent.
        /// </summary>
        /// <param name="state"> current state </param>
        public static void SendCallBack(IAsyncResult state)
        {
            // StateObject stateO = (StateObject)state.AsyncState;
            Socket socket = (Socket)state.AsyncState;

            // Get buffer that we tried to send
            //byte[] outgoingBuffer = (byte[])state.AsyncState;

            int bytesSent = socket.EndSend(state);
            // System.Diagnostics.Debug.WriteLine(bytesSent + " bytes sent");
            // The socket has been closed
            if (bytesSent == 0)
            {
                //close socket if no bytes being sent?
                socket.Close();
                System.Diagnostics.Debug.WriteLine("socket closed");
            }
        }
    }
}
