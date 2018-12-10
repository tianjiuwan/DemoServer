using System;
using System.Collections.Generic;

/// <summary>
/// 2维向量
/// </summary>
public class Vector2
{
    public float x;
    public float y;

    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    //距离
    public static double distance(Vector2 from, Vector2 to)
    {
        float sx = from.x - to.x;
        float sy = from.y - to.x;
        return Math.Sqrt(sx * sx + sy * sy);
    }
    //方向
    public static Vector2 direction(Vector2 from, Vector2 to)
    {
        return new Vector2(to.x - from.x, to.y - from.y);
    }

}

