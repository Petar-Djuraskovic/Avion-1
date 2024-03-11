using System;

public class Cube
{
	private float sideLength;
	private float volume;

	public Cube(sideLength)
	{
		this.sideLength = sideLength;
		this.volume = this.CubedNumber(sideLength);
	}

	public float GetVolume()
    {
		return volume;
    }

	public void SetVolume(float volume)
    {
		this.volume = volume;
		this.sideLength = Math.Cbrt(volume);
    }

	public float GetSideLength()
    {
		return this.sideLength;
    }

	public void SetSideLength(sideLength)
    {
		this.sideLength = sideLength;
		this.volume = this.CubedNumber(sideLength);
    }

	public static float CubedNumber(a)
    {
		return a * a * a;
    }
}


float x = 3f;
float xCubed = myCube.CubedNumber(x);



