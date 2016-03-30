package asteroids.participants;

import static asteroids.Constants.*;

import java.awt.Shape;
import java.awt.geom.AffineTransform;
import java.awt.geom.Path2D;

import asteroids.Controller;
import asteroids.Participant;
import asteroids.ParticipantCountdownTimer;
import asteroids.destroyers.*;

/**
 * Represents alien ships
 */
public class AlienShip extends Participant implements ShipDestroyer, AsteroidDestroyer, BulletDestroyer
{

	//outline of the alien ship
	private Shape outline;
	
	//Type of alien ship
	//0 is small, 1 is big
	private int type;
	
	//Game controller
	private Controller controller;
	
	//Creates an alien ship at the given coordinates of given type
	public AlienShip(double x, double y, int type, Controller controller)
	{
		setPosition(x,y);
		this.type = type;
		this.controller = controller;
		this.setVelocity(3, RANDOM.nextDouble()*Math.PI*2);
		
		Path2D.Double poly = new Path2D.Double();
		poly.moveTo(20, 0);
		poly.lineTo(13,-8);
		poly.lineTo(-13,-8);
		poly.lineTo(-20,0);
		poly.closePath();
		poly.moveTo(20, 0);
		poly.lineTo(10,8);
		poly.lineTo(-10,8);
		poly.lineTo(-20,0);
		poly.closePath();
		poly.moveTo(-10,-8);
		poly.lineTo(-7,-15);
		poly.lineTo(7,-15);
		poly.lineTo(10, -8);
		poly.closePath();
		outline = poly;
		
		if(type < 0 || type > 1)
		{
			throw new IllegalArgumentException("Invalid alien ship type");
		}
		
		 // Scale to the desired size
        double scale = ALIENSHIP_SCALE[type];
        poly.transform(AffineTransform.getScaleInstance(scale, scale));
		
		new ParticipantCountdownTimer(this, "move", 2000);
		if(type == 1)
		{
			new ParticipantCountdownTimer(this, "shoot", 2500);
		}
		else if(type == 0)
		{
			new ParticipantCountdownTimer(this, "target", 2500);
		}
		
		
	}
	
	/**
	 * Returns the type of alienShip
	 * 0 is small ship
	 * 1 is big ship
	 */
	public int getType()
	{
		return type;
	}
	

	/**
	 * returns the outline of the alien ship
	 */
	@Override
	protected Shape getOutline() {
		
		return outline;
	}

	/**
	 * When an alien ship collides with an alien destroyer it expires
	 */
	@Override
	public void collidedWith(Participant p) {
		
		if(p instanceof AlienDestroyer)
		{
			//Inform controller
		    controller.alienDestroyed(this.getType());
			
			//Expire this alien ship from the game
			Participant.expire(this);
				
		}		
	}
	
    /**
     * This method is invoked when a ParticipantCountdownTimer completes
     * its countdown.
     */
    @Override
    public void countdownComplete (Object payload)
    {
        //Expire bullet after 1 sec
        if (payload.equals("move"))
        {
            this.setDirection(RANDOM.nextDouble()*Math.PI*2);
            new ParticipantCountdownTimer(this, "move", 2000);
        }
        else if(payload.equals("shoot"))
        {
        	//Shoot bullet in random direction
        	controller.shootAlienBullet();
        	new ParticipantCountdownTimer(this, "shoot", 2500);
        }
        else if(payload.equals("target"))
        {
        	//Shoot bullet and target ship
        	controller.targetAlienBullet();
        	new ParticipantCountdownTimer(this, "target", 2500);
        	
        }
    }

}
