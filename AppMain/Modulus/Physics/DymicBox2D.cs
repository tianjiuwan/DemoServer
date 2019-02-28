namespace TwlPhy
{
    using System;

    public class DymicBox2D : Box2D
    {
        public DymicBox2D(float x, float y, float w, float h) : base(x, y, w, h)
        {

        }

        //重写 锚点在下方
        protected override void refresh()
        {
            left = centerX - width / 2;
            right = centerX + width / 2;
            top = centerY + height;
            bottom = centerY;
        }

        public void UpdateCenter(float x, float y)
        {
            centerX = x;
            centerY = y;
            refresh();
        }

        public void OnlyUpdateCenter(float x, float y)
        {
            centerX = x;
            centerY = y;
        }

        public void AddCenter(float x, float y)
        {
            centerX += x;
            centerY += y;
            refresh();
        }

        public void UpdateSize(float w, float h)
        {
            width = w;
            height = h;
            refresh();
        }

    }
}
