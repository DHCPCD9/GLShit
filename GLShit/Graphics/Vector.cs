namespace GLShit.Graphics;

public class Vector
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Vector() {
        X = 0;
        Y = 0;
    }

}