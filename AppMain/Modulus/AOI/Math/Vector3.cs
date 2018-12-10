using System;
using System.Collections.Generic;

/// <summary>
/// 2维向量
/// </summary>
public class Vector3
{
    public float x;
    public float y;
    public float z;

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    //距离
    public static double distance(Vector3 from, Vector3 to)
    {
        float sx = from.x - to.x;
        float sy = from.y - to.x;
        float sz = from.z - to.z;
        return Math.Sqrt(sx * sx + sy * sy + sz * sz);
    }
    //方向
    public static Vector3 direction(Vector3 from, Vector3 to)
    {
        return new Vector3(to.x - from.x, to.y - from.y, to.z - from.z);
    }

}

