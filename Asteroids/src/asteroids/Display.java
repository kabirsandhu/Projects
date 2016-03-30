package asteroids;

import javax.swing.*;
import java.awt.*;
import static asteroids.Constants.*;

/**
 * Defines the top-level appearance of an Asteroids game.
 */
@SuppressWarnings("serial")
public class Display extends JFrame
{
    // The area where the action takes place
    private Screen screen;
    
    //The main panel
    private JPanel mainPanel = new JPanel();
    
    // This panel contains buttons and labels
    JPanel controls = new JPanel();
    

   

    /**
     * Lays out the game and creates the controller
     */
    public Display (Controller controller)
    {
        // Title at the top
        setTitle(TITLE);

        // Default behavior on closing
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        // The main playing area and the controller
        screen = new Screen(controller);
        
        // This panel contains the screen to prevent the screen from being
        // resized
        JPanel screenPanel = new JPanel();
        screenPanel.setLayout(new GridBagLayout());
        screenPanel.add(screen);
        
        controls.setLayout(new GridBagLayout());
        GridBagConstraints c = new GridBagConstraints();
        c.insets = new Insets(1,1,5,1);
        
        // The button that starts the game
        JButton startGame = new JButton(START_LABEL);
        GridBagConstraints startConstraint = new GridBagConstraints();
        startConstraint.insets = new Insets(5,50,5,465);
        controls.add(startGame, startConstraint);
        
        
        // Organize everything    
        mainPanel.setLayout(new BorderLayout());
        mainPanel.add(screenPanel, "Center");
        mainPanel.add(controls, "North");
        setContentPane(mainPanel);
        pack();
        
        // Connect the controller to the start button
        startGame.addActionListener(controller);


    }
    
    /**
     * Called when it is time to update the screen display. This is what drives
     * the animation.
     */
    public void refresh ()
    {
        screen.repaint();
    }
    
    /**
     * Sets the large legend
     */
    public void setLegend (String s)
    {
        screen.setLegend(s);
    }
    
    
    /**
     * Updates the labels
     */
    public void updateLabels(Controller controller){
    	
    	mainPanel.remove(controls);
    	controls = new JPanel();
        
        controls.setLayout(new GridBagLayout());
        GridBagConstraints startConstraint = new GridBagConstraints();
        GridBagConstraints labelConstraint = new GridBagConstraints();
        startConstraint.insets = new Insets(5,50,5,300);
        labelConstraint.insets = new Insets(5, 5, 10, 5);
        
        // The button that starts the game
        JButton startGame = new JButton(START_LABEL);
        controls.add(startGame,startConstraint);
        
        //The current level
        JLabel level = new JLabel("Level: " + controller.getLevel());
        controls.add(level, labelConstraint);
        
        //Number of lives remaining
        JLabel lives = new JLabel("Lives: " + controller.getLives());
        controls.add(lives, labelConstraint);
    	        
        //The score
        JLabel score = new JLabel("Score: " + controller.getScore());
        controls.add(score, labelConstraint);
        
        //Add labels to main panel
        mainPanel.add(controls, "North");
        setContentPane(mainPanel);
        pack();
        
        // Connect the controller to the start button
        startGame.addActionListener(controller);

    }
}
