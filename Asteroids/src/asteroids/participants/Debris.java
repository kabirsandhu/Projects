package asteroids.participants;

import static asteroids.Constants.*;

import java.awt.Shape;
import java.awt.geom.Path2D;

import asteroids.Participant;
import asteroids.ParticipantCountdownTimer;

/**
 * Represents debris 
 */
public class Debris extends Participant
{
	//outline of the debris
	private Shape outline;
	
	/**
	 * Creates debris at specified x and y pointed in the given direction
	 * debris type 0 represents particles
	 * debris type 1 represents rods
	 */
	public Debris(int type, double x, double y, double direction)
	{
		setPosition(x,y);
		setRotation(RANDOM.nextDouble()*Math.PI);
		setVelocity(.5, direction);
		Path2D.Double poly = new Path2D.Double();
		 
		if(type == 0)
		{
			poly.moveTo(.1, .1);
			poly.lineTo(-.1, .1);
			poly.lineTo(-.1, -.1);
			poly.lineTo(.1, -.1);
			poly.closePath();
			outline = poly;
		}
		
		if(type == 1)
		{
			poly.moveTo(0,0);
			poly.lineTo(RANDOM.nextInt(10), RANDOM.nextInt(10));
			outline = poly;
		}
		
		new ParticipantCountdownTimer(this, "expire", 2000);
	}

	/**
	 * returns outline of the debris
	 */
	@Override
	protected Shape getOutline() {
		return outline;
	}

	@Override
	public void collidedWith(Participant p) {	
		
	}
	
    /**
     * This method is invoked when a ParticipantCountdownTimer completes
     * its countdown.
     */
    @Override
    public void countdownComplete (Object payload)
    {
        //Expire debris after 2 sec
        if (payload.equals("expire"))
        {
            Participant.expire(this);
        }
    }

}
