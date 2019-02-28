namespace TwlPhy
{
    using System;
    /// <summary>
    ///物理系统不与Unity耦合 
    /// </summary>
    public class Physics2D
    {
        static public bool isCollision(Box2D p1, Box2D p2)
        {
            return !(p2.Top <= p1.Bottom || p2.Bottom >= p1.Top || p2.Left >= p1.Right || p2.Right <= p1.Left);
        }

        static public bool isCollision(Box2D box)
        {
            return LevelMgr.Instance.NowLevel.isCollision(box);
        }

        static public bool isCollision(Box2D box, Vector2 dir, float dst, ref float len)
        {
            return LevelMgr.Instance.NowLevel.isCollision(box, dir, dst, ref len);
        }
    }

    public class Physics3D
    {
        static public bool isCollision(DymicBox3D p1, DymicBox3D p2)
        {
            return !(p2.Top <= p1.Bottom || p2.Bottom >= p1.Top || p2.Left >= p1.Right || p2.Right <= p1.Left || p2.MaxH <= p1.MinH || p2.MinH >= p1.MaxH);
        }
    }
}