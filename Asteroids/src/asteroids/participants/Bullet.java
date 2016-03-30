package asteroids.participants;

import java.awt.Shape;
import java.awt.geom.*;

import asteroids.Participant;
import static asteroids.Constants.*;
import asteroids.ParticipantCountdownTimer;
import asteroids.destroyers.*;

/**
 * represents bullets fired by the ship
 */
public class Bullet extends Participant implements AsteroidDestroyer, AlienDestroyer 
{
	//outline of the bullet
	private Shape outline;

	//Constructs a bullet at the specified location and pointed in the given direction
	public Bullet(double x, double y, double direction){
		
		setPosition(x,y);
		setVelocity(BULLET_SPEED, direction);
		
		Path2D.Double poly = new Path2D.Double();
		poly.moveTo(.5, .5);
		poly.lineTo(-.5, .5);
		poly.lineTo(-.5, -.5);
		poly.lineTo(.5, -.5);
		poly.closePath();
		outline = poly;
		
		new ParticipantCountdownTimer(this, "expire", BULLET_DURATION);
	}
	
	/**
	 * returns the outline of the bullet
	 */
	@Override
	protected Shape getOutline() 
	{
		return outline;
	}

	/**
	 * When a bullet collides with a bullet destroyer it expires
	 */
	@Override
	public void collidedWith(Participant p) 
	{
        if (p instanceof BulletDestroyer)
        {
            // Expire the bullet from the game
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
        if (payload.equals("expire"))
        {
            Participant.expire(this);
        }
    }

}
