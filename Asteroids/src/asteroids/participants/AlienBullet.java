package asteroids.participants;

import static asteroids.Constants.*;

import java.awt.Shape;
import java.awt.geom.Path2D;

import asteroids.Participant;
import asteroids.ParticipantCountdownTimer;
import asteroids.destroyers.*;

/**
 * Represents bullets fired by alien ship
 */
public class AlienBullet extends Participant implements ShipDestroyer, AsteroidDestroyer
{
	//outline of the bullet
	private Shape outline;
	
	
	//constructs bullet at the specified coordinates that is pointed in the given direction
	public AlienBullet(double x, double y, double direction)
	{
		setPosition(x, y);
		setDirection(0);
		setRotation(0);
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
	protected Shape getOutline() {
		return outline;
	}

	/**
	 * When a bullet collides with an alienDestroyer it expires
	 */
	@Override
	public void collidedWith(Participant p) {
		if(p instanceof AlienDestroyer)
		{
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
