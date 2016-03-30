package cs1410;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.util.Arrays;
import java.util.Random;

import javax.swing.*;

public  class LightsOut implements ActionListener {
	
	/**
	 * Starts a new lights out game
	 */
	public static void main(String[] args){
		try
		{
		    UIManager.setLookAndFeel(UIManager.getCrossPlatformLookAndFeelClassName());
		}
		catch(Exception e)
		{
		}
		new LightsOut();
	}
	
	private boolean manualSetup = false;
	private JButton[] buttons = new JButton[25];
	private boolean[] lights = new boolean[25];
	private JButton manualSetupButton = new JButton("Enter Manual Setup");
	 
	/**
	 * Creates a  Lights out game gui
	 */
	public LightsOut(){
		
		//Set up frame 
		JFrame frame = new JFrame();
        frame.setTitle("Lights Out");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setPreferredSize(new Dimension(350,400));
        frame.setMinimumSize(new Dimension(350,400));
        
        //Add Content Pane
        JPanel mainPanel = new JPanel();
        BorderLayout layout = new BorderLayout();
        mainPanel.setLayout(layout);
        frame.setContentPane(mainPanel);
        
        //Add board and lights
        JPanel board = new JPanel();
        GridLayout grid = new GridLayout(5,5);
        board.setLayout(grid);
        int count = 0;
        for(int j = 0; j < 5; j++){
        	for(int i = 0; i < 5; i++){
        		JButton light = new JButton();
        		light.setBackground(Color.BLUE);
        		light.setName("" + count);
        		light.addActionListener(this);
        		buttons[count] = light;
        		lights[count] = false;
        		board.add(buttons[count]);
        		count++;
        	}
        }        
        mainPanel.add(board, "Center");
        
        //Add New Game and Manual Setup Buttons 
        JPanel bot = new JPanel();
        bot.setLayout(new GridLayout(1,0));
        JButton newGame = new JButton("New Game");
        newGame.addActionListener(this);
        bot.add(newGame);
        manualSetupButton.addActionListener(this);
        bot.add(manualSetupButton);
        mainPanel.add(bot, "South");
       
        //Create a new game
		Random rand = new Random();
		for(JButton b : buttons){
			b.setBackground(Color.BLUE);
		}
		int moves = rand.nextInt(100);
		for( int i = 0; i < moves; i++){
			move(rand.nextInt(24));
		}
        
        
        // Display
        frame.pack();
        frame.setVisible(true);
        
	}
	
	/**
	 * Gets the button that was pressed.
	 * Toggles manual setup mode if the manual setup button is pressed.
	 * Creates a random game configuration if the new game button is pressed.
	 * Changes the game board if one of the game board buttons is pressed.
	 */
	public void actionPerformed(ActionEvent e)
	{
		//Get the button that was clicked
		JButton button = (JButton) e.getSource();
		
		//If Manual setup button clicked toggles manual setup mode
		if(button.getText().equals("Enter Manual Setup")){
			manualSetupButton.setText("Exit Manual Setup");
			manualSetup = true;		
		}
		
		else if(button.getText().equals("Exit Manual Setup")){
			manualSetupButton.setText("Enter Manual Setup");
			manualSetup = false;
		}
		
		//If New game button clicked creates a random board
		else if(button.getText().equals("New Game")){
			Random rand = new Random();
			for(JButton b : buttons){
				b.setBackground(Color.BLUE);
			}
			int moves = rand.nextInt(100);
			for( int i = 0; i < moves; i++){
				move(rand.nextInt(24));
			}
				
		}
		//If Game board button clicked changes lights accordingly
		else{
			int index = Integer.parseInt(button.getName());
			move(index);
		}
	}
	
	/**
	 * Takes an index as a parameter and changes the color of the button at the index in buttons[] 
	 * and changes the surrounding buttons if manual setup is off.
	 * Changes just the one button if manual setup is on. 
	 * Displays a dialog box if the game is won.
	 * Throws illegalArgumentException if index is out of range.
	 */
	public void move(int index){
		if(index > 24 || index < 0){
			throw new IllegalArgumentException();
		}
		if(!manualSetup){
			changeLight(buttons[index]);
			if(index != 4 && index != 9 && index != 14 && index != 19 && index != 24){
				changeLight(buttons[index + 1]);
			}
			if(index != 0 && index != 5 && index != 10 && index != 15 && index != 20){
				changeLight(buttons[index - 1]);
			}
			if(index > 4){
				changeLight(buttons[index - 5]);
			}
			if(index < 20){
				changeLight(buttons[index + 5]);
			}
			if(isWon()){
				JOptionPane.showMessageDialog(null, "You Win!");
			}
		}
		else{
			changeLight(buttons[index]);
		}
		
	}
	
	/**
	 * Changes the color of the button
	 */
	public void changeLight(JButton b){
		if(b.getBackground() == Color.BLUE){
			b.setBackground(Color.YELLOW);
		}
		else if (b.getBackground() == Color.YELLOW){
			b.setBackground(Color.BLUE);
		}
		
	}
	
	/**
	 * Checks if the game is won and returns true if it is 
	 */
	public boolean isWon(){
		for(JButton b : buttons){
			if(b.getBackground() == Color.YELLOW){
				return false;
			}
		}
		return true;
	}

}
