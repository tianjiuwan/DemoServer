namespace TwlPhy
{
    using System;

    public class Box2D
    {
        public float centerX
        {
            protected set;
            get;
        }
        public float centerY
        {
            protected set;
            get;
        }
        public float width
        {
            protected set;
            get;
        }
        public float height
        {
            protected set;
            get;
        }

        public Box2D(float x, float y, float w, float h)
        {
            centerX = x;
            centerY = y;
            width = w;
            height = h;
            refresh();
        }

        //BOX的锚点在中心
        protected virtual void refresh()
        {
            left = centerX - width / 2;
            right = centerX + width / 2;
            top = centerY + height / 2;
            bottom = centerY - height / 2;
        }

        protected float left;
        public float Left
        {
            get
            {
                return left;
            }
        }

        protected float right;
        public float Right
        {
            get
            {
                return right;
            }
        }

        protected float top;
        public float Top
        {
            get
            {
                return top;
            }
        }

        protected float bottom;
        public float Bottom
        {
            get
            {
                return bottom;
            }
        }

    }

}