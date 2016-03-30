package asteroids;

import java.awt.event.*;
import java.util.Iterator;

import javax.swing.*;

import asteroids.participants.*;
import static asteroids.Constants.*;

/**
 * Controls a game of Asteroids.
 */
public class Controller implements KeyListener, ActionListener
{
    // The state of all the Participants
    private ParticipantState pstate;
    
    // The ship (if one is active) or null (otherwise)
    private Ship ship;
    
    //The alien ship (if one is active) or null (otherwise)
    private AlienShip alienShip;
    
    //Turn or accelerate ship when true
    private boolean turnRight;
    private boolean turnLeft;
    private boolean accelerate;
    
    //Show flame behind ship when true
    private boolean showFlame;
    
    //Time to transition to nextLevel if true
    private boolean changeLevel;
    
    // When this timer goes off, it is time to refresh the animation
    private Timer refreshTimer;
    
    //When this timer goes off, an alien ship is placed
    private Timer alienTimer;

    // The time at which a transition to a new stage of the game should be made.
    // A transition is scheduled a few seconds in the future to give the user
    // time to see what has happened before doing something like going to a new
    // level or resetting the current level.
    private long transitionTime;
    
    // Number of lives left
    private int lives;
    
    //Current Score
    private int score;
    
    //Current level
    private int level;

    // The game display
    private Display display;
    

    /**
     * Constructs a controller to coordinate the game and screen
     */
    public Controller ()
    {
        // Record the game and screen objects
        display = new Display(this);
        display.setVisible(true);        
        
        // Initialize the ParticipantState
        pstate = new ParticipantState();

        // Set up the refresh timer.
        refreshTimer = new Timer(FRAME_INTERVAL, this);

        // Clear the transitionTime
        transitionTime = Long.MAX_VALUE;

        // Bring up the splash screen and start the refresh timer
        splashScreen();
        refreshTimer.start();
    }

    /**
     * Returns the ship, or null if there isn't one
     */
    public Ship getShip ()
    {
        return ship;
    }

    /**
     * Configures the game screen to display the splash screen
     */
    private void splashScreen ()
    {
        // Clear the screen, reset the level, and display the legend
        clear();
        display.setLegend("Asteroids");

        // Place four asteroids near the corners of the screen.
        placeAsteroids(3);
    }

    /**
     * The game is over. Displays a message to that effect.
     */
    private void finalScreen ()
    {
        display.setLegend(GAME_OVER);
        display.removeKeyListener(this);
        Participant.expire(alienShip);
    }

    /**
     * Place a new ship in the center of the screen. Remove any existing ship
     * first.
     */
    private void placeShip ()
    {
        // Place a new ship
        Participant.expire(ship);
        ship = new Ship(SIZE / 2, SIZE / 2, -Math.PI / 2, this);
        addParticipant(ship);
        display.setLegend("");
        turnRight = false;
        turnLeft = false;
        accelerate = false;
        showFlame = false;
       
    }

    /**
     * Places four asteroids near the corners of the screen. Gives them random
     * velocities and rotations.
     */
    private void placeAsteroids (int speed)
    {
        addParticipant(new Asteroid(0, 2, EDGE_OFFSET, EDGE_OFFSET, speed, this));
        addParticipant(new Asteroid(1, 2, SIZE - EDGE_OFFSET, EDGE_OFFSET, speed, this));
        addParticipant(new Asteroid(2, 2, EDGE_OFFSET, SIZE - EDGE_OFFSET, speed, this));
        addParticipant(new Asteroid(3, 2, SIZE - EDGE_OFFSET, SIZE - EDGE_OFFSET, speed, this));
    }
    
    /**
     * Places a bullet at the nose of the ship with an initial velocity if the number of bullets already on screen is less than 8.
     */
    private void placeBullet()
    {
    	if(pstate.countBullets() < BULLET_LIMIT)
    	{
    		addParticipant(new Bullet(ship.getXNose(), ship.getYNose(), ship.getRotation()));	
    	}
    }
    
    /**
     * Places an alien ship along the edge of the screen
     * Places a big alien ship if it is level 2 otherwise places a small one 
     */
    private void placeAlien()
    {
    	int rand = RANDOM.nextInt(4);
    	int type = 0;
    	if(level == 2)
    	{
    		type = 1;
    	}
    	
    	
    	if(rand == 0)
    	{
    		//Place on left edge
    		alienShip = new AlienShip(0, RANDOM.nextDouble()*750, type, this);
    		addParticipant(alienShip);
    	}
    	else if(rand == 1)
    	{
    		//Place on right edge
    		alienShip = new AlienShip(750, RANDOM.nextDouble()*750, type, this);
    		addParticipant(alienShip);
    	}
    	else if(rand == 2)
    	{
    		//Place on top edge
    		alienShip = new AlienShip(RANDOM.nextDouble()*750,0, type, this);
    		addParticipant(alienShip);
    	}
    	else
    	{
    		//Place on bottom edge
    		alienShip = new AlienShip(RANDOM.nextDouble()*750,750, type, this);
    		addParticipant(alienShip);
    	}
    }
    
    /**
     * Places an alien bullet that fires in a random direction
     */
    public void shootAlienBullet()
    {
    	if(alienShip != null)
    	{
    		addParticipant(new AlienBullet(alienShip.getX(), alienShip.getY(), RANDOM.nextDouble()*2*Math.PI));
    	}
    }
    
    /**
     * Places an alien bullet that targets the ship if the ship is not null
     */
    public void targetAlienBullet()
    {
    	if(ship!= null)
    	{
    		double xDifference = ship.getX() - alienShip.getX();
    		double yDifference = ship.getY() - alienShip.getY();
    		double direction = Math.atan((yDifference) / (xDifference));
    		if(xDifference >= 0 && yDifference >= 0){
    			addParticipant(new AlienBullet(alienShip.getX(), alienShip.getY(), direction));
    		}
    		else if((xDifference <= 0) && (yDifference >= 0))
    		{
    			addParticipant(new AlienBullet(alienShip.getX(), alienShip.getY(), direction - Math.PI));
    		}
    		else if(xDifference >= 0 && yDifference <= 0){
    			addParticipant(new AlienBullet(alienShip.getX(), alienShip.getY(), direction));
    		}
    		else
    		{
    			addParticipant(new AlienBullet(alienShip.getX(), alienShip.getY(), direction + Math.PI));
    		}
    	}
    }
    


    /**
     * Clears the screen so that nothing is displayed
     */
    private void clear ()
    {
        pstate.clear();
        display.setLegend("");
        ship = null;
        alienShip = null;
    }

    /**
     * Sets things up and begins a new game.
     */
    private void initialScreen ()
    {
        // Clear the screen
        clear();

        // Place four asteroids
        placeAsteroids(3);

        // Place the ship
        placeShip();

        // Reset lives to three
        lives = 3;
        
        //Reset Score
        score = 0;
        
        //Set level to one
        level = 1;
        
        
        //Don't change level
        changeLevel = false;
        
        // Start listening to events (but don't listen twice)
        display.removeKeyListener(this);
        display.addKeyListener(this);

        // Give focus to the game screen
        display.requestFocusInWindow();
        
        //Update labels
        display.updateLabels(this);
    }

    /**
     * Adds a new Participant
     */
    public void addParticipant (Participant p)
    {
        pstate.addParticipant(p);
    }

    /**
     * The ship has been destroyed
     */
    public void shipDestroyed ()
    {
        
        // Decrement lives
        lives--;
        
        //Create debris
        addParticipant(new Debris(1, ship.getX(), ship.getY(), RANDOM.nextDouble() * (Math.PI * 2)));
        addParticipant(new Debris(1, ship.getX(), ship.getY(), RANDOM.nextDouble() * (Math.PI * 2)));
        addParticipant(new Debris(1, ship.getX(), ship.getY(), RANDOM.nextDouble() * (Math.PI * 2)));
        
        // Null out the ship
        ship = null;

        // Since the ship was destroyed, schedule a transition
        scheduleTransition(END_DELAY);
        display.updateLabels(this);
    }

    /**
     * An asteroid of the given size has been destroyed
     */
    public void asteroidDestroyed (int size, double x, double y)
    {
    	//Update score
    	score += ASTEROID_SCORE[size];
    	display.updateLabels(this);
    	
    	//Create debris	
    	addParticipant(new Debris(0, x, y, RANDOM.nextDouble()*(Math.PI * 2)));
    	addParticipant(new Debris(0, x, y, RANDOM.nextDouble()*(Math.PI * 2)));
    	addParticipant(new Debris(0, x, y, RANDOM.nextDouble()*(Math.PI * 2)));
    	addParticipant(new Debris(0, x, y, RANDOM.nextDouble()*(Math.PI * 2)));
    	
        // If all the asteroids are gone, schedule a transition
        if (pstate.countAsteroids() == 0)
        {
        	//Increment level
        	level++;
        	
        	//Transition to next level
        	changeLevel = true;
            scheduleTransition(END_DELAY);
            
            //Update labels
            display.updateLabels(this);          
        }
    }
    
    /**
     * An alien ship of the given type has been destroyed
     * type = 0 means small ship and type = 1 means big ship
     */
    public void alienDestroyed(int type)
    {
    	//Update score
    	score += ALIENSHIP_SCORE[type];
    	display.updateLabels(this);
    	alienTimer = new Timer(ALIEN_DELAY, this);
    	alienTimer.start();
    	
    	//Create debris
    	addParticipant(new Debris(1, alienShip.getX(), alienShip.getY(), RANDOM.nextDouble()*(Math.PI * 2)));
    	addParticipant(new Debris(1, alienShip.getX(), alienShip.getY(), RANDOM.nextDouble()*(Math.PI * 2)));
    	addParticipant(new Debris(1, alienShip.getX(), alienShip.getY(), RANDOM.nextDouble()*(Math.PI * 2)));
    	addParticipant(new Debris(1, alienShip.getX(), alienShip.getY(), RANDOM.nextDouble()*(Math.PI * 2)));
    	addParticipant(new Debris(1, alienShip.getX(), alienShip.getY(), RANDOM.nextDouble()*(Math.PI * 2)));
    	
    	//null out alienShip
    	alienShip = null;
    }


    /**
     * Schedules a transition m msecs in the future
     */
    private void scheduleTransition (int m)
    {
        transitionTime = System.currentTimeMillis() + m;
    }

    /**
     * This method will be invoked because of button presses and timer events.
     */
    @Override
    public void actionPerformed (ActionEvent e)
    {
        // The start button has been pressed. Stop whatever we're doing
        // and bring up the initial screen
        if (e.getSource() instanceof JButton)
        {
            initialScreen();
        }

        // Time to refresh the screen and deal with keyboard input
        else if (e.getSource() == refreshTimer)
        {
            // It may be time to make a game transition
            performTransition();

            // Move the participants to their new locations
            pstate.moveParticipants();

            // Refresh screen
           display.refresh();
        }
        
        //Time to place alien
        else if(e.getSource() == alienTimer)
        {
        	alienTimer.stop();
        	placeAlien();
        }
    }

    /**
     * Returns an iterator over the active participants
     */
    public Iterator<Participant> getParticipants ()
    {
        return pstate.getParticipants();
    }

    /**
     * If the transition time has been reached, transition to a new state
     */
    private void performTransition ()
    {
    	
    	//Turn the ship if necessary
    	if(turnRight && ship != null)
    	{
        		ship.turnRight();
    	}
   	 
    	if(turnLeft && ship != null)
    	{
        		ship.turnLeft();
    	}
   	 
   	 
    	//Accelerate ship if necessary and show flame every other frame
   	 	if(accelerate && ship != null)
   	 	{
   	 		ship.accelerate();
   	 		if(showFlame)
   	 		{
   			 
   	 			showFlame = false;
   	 		}
   	 		else
   	 		{
   	 			showFlame = true;
   	 		}
   	 	}
   	 

   	 	//Show flame if necessary
   	 	if(showFlame && ship != null)
   	 	{
   	 		ship.flame();
   		
   	 	}
   	 
   	 	//Remove flame when necessary
   	 	if((!showFlame || !accelerate) && ship != null){
   	 		ship.noFlame();
   	 	}
    	 
   	 	// Do something only if the time has been reached
   	 	if (transitionTime <= System.currentTimeMillis())
   	 	{
   	 		// Clear the transition time
   	 		transitionTime = Long.MAX_VALUE;
   	 		
   	 		
   	 		if(changeLevel && level >= 2)
   	 		{
   	 			//Clear the screen
   	 			clear();
   	 			changeLevel = false;
   	 			
   	 			//Reset the ship
   	 			placeShip();
   	 			
   	 			//Reset asteroids and increase their speed
   	 			placeAsteroids(level + 2);
   	 			
   	 			//Start alien timer
   	 			alienTimer = new Timer(ALIEN_DELAY, this);
   	 			alienTimer.start();
   	 		}

           	// If there are no lives left, the game is over. Show the final
   	 		// screen.
            if (lives <= 0)
            {
            	finalScreen();
            }

            // If the ship was destroyed, place a new one and continue
            else if (ship == null)
            {
            	placeShip();
            }           
   	 	}
    }
    


    /**
     * If a key of interest is pressed, record that it is down.
     */
    @Override
    public void keyPressed (KeyEvent e)
    {
    	//Turn left if left or a key pressed
        if ((e.getKeyCode() == KeyEvent.VK_LEFT || e.getKeyCode() == KeyEvent.VK_A) && ship != null)
        {
        	turnLeft = true;
        	performTransition();
        }
        
        //Turn right if right or d key pressed
        else if ((e.getKeyCode() == KeyEvent.VK_RIGHT || e.getKeyCode() == KeyEvent.VK_D) && ship != null)
        {
            turnRight = true;
            performTransition();
        }
        
        //Accelerate if up or w key is pressed
        else if((e.getKeyCode() == KeyEvent.VK_UP || e.getKeyCode() == KeyEvent.VK_W) && ship != null)
        {
        	accelerate = true;       	
        	performTransition();
        }
        //Place a bullet if space, down, or s key pressed
        else if((e.getKeyCode() == KeyEvent.VK_SPACE || e.getKeyCode() == KeyEvent.VK_DOWN || e.getKeyCode() == KeyEvent.VK_S) && ship != null){
        	placeBullet();
        }
    }

    /**
     * Ignore these events.
     */
    @Override
    public void keyTyped (KeyEvent e)
    {
    }

    /**
     * If a key of interest is released, record that it is up.
     */
    @Override
    public void keyReleased (KeyEvent e)
    {    
    	//Stop turning left when left or a key released
    	if((e.getKeyCode() == KeyEvent.VK_LEFT || e.getKeyCode() == KeyEvent.VK_A) && ship != null){
    		turnLeft = false;
    	}
    	//Stop turning right if right or d key released
    	else if((e.getKeyCode() == KeyEvent.VK_RIGHT || e.getKeyCode() == KeyEvent.VK_D) && ship != null){
    		turnRight = false;
    	}
    	//Stop accelerating if up or w key released
    	else if((e.getKeyCode() == KeyEvent.VK_UP || e.getKeyCode() == KeyEvent.VK_W) && ship != null)
    	{
    		accelerate = false;
    		
    	}
    }
    
    /**
     * Returns the current score
     */
    public int getScore()
    {
    	return score;
    }
    
    /**
     * Returns the number of lives remaining
     */
    public int getLives()
    {
    	return lives;
    }
    
    /**
     * Returns the current level
     */
    public int getLevel()
    {
    	return level;
    }
}
