namespace TwlPhy
{
    using System;

    public class DymicBox3D : DymicBox2D
    {
        public DymicBox3D(float x, float y, float w, float h) : base(x, y, w, h)
        {

        }

        public DymicBox3D(float x, float y, float w, float h, float height) : base(x, y, w, h)
        {
            //左右上下还是与2D相同
            //多了2个 3D空间中的最高与最低
            minH = y;
            maxH = height;
        }

        //用其他的碰撞盒复制+偏移 x,y坐标偏移z高度偏移
        public DymicBox3D(DymicBox3D copy, Vector3 offset) : this(copy.centerX + offset.x, copy.centerY + offset.y, copy.width, copy.height, copy.MaxH + offset.z)
        {

        }

        public DymicBox3D(float x, float y, Vector3 size, Vector3 offset) : this(x + offset.x, y + offset.y, size.x, size.y, size.z + offset.z)
        {

        }

        public void UpdateHeight(float height)
        {
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
