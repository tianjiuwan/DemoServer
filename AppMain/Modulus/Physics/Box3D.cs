namespace TwlPhy
{
    using System;

    public class Box3D : Box2D
    {
        //在2D平面加了一个高度
        //计算碰撞 6个面
        public Box3D(float x, float y, float w, float h) : base(x, y, w, h)
        {

        }

        public Box3D(float x, float y, float w, float h, float height) : base(x, y, w, h)
        {
            //左右上下还是与2D相同
            //多了2个 3D空间中的最高与最低
            minH = y;
            maxH = height;
        }

        protected float minH;
        public float MinH
        {
            get
            {
                return minH;
            }
        }

        protected float maxH;
        public float MaxH
        {
            get
            {
                return maxH;
            }
        }

    }
}
