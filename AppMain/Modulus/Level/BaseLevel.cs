using System.Collections;
using System.Collections.Generic;
using TwlPhy;
using System;

/// <summary>
/// 关卡代码也需要在服务器用
/// 关卡记录静态碰撞信息
/// </summary>
public class BaseLevel
{
    private List<Box2D> colliders = new List<Box2D>();
    public List<Box2D> Colliders
    {
        get
        {
            return colliders;
        }
    }
    private DymicBox2D dyBox = new DymicBox2D(0, 0, 0, 0);

    public void addBox(Box2D box)
    {
        if (!colliders.Contains(box))
        {
            colliders.Add(box);
        }
    }
    public void addBox(List<Box2D> lst)
    {
        colliders.AddRange(lst);
    }

    /// <summary>
    /// box是否与场景物体碰撞
    /// </summary>
    /// <param name="box"></param>
    /// <returns></returns>
    public bool isCollision(Box2D box)
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            if (Physics2D.isCollision(box, colliders[i]))
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// 计算位移碰撞 
    /// 计算出最大移动距离
    /// 缺陷:速度1帧超过了碰撞器大小 无法验证 应该没有那么变态
    /// </summary>
    /// <param name="box"></param>
    /// <param name="dir"></param>
    /// <param name="dst"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public bool isCollision(Box2D box, Vector2 dir, float dst, ref float len)
    {
        //copy一个出来
        float offsetX = dir.x * dst;
        float offsetY = dir.y * dst;
        dyBox.OnlyUpdateCenter(box.centerX + offsetX, box.centerY + offsetY);
        dyBox.UpdateSize(box.width, box.height);
        len = dst;
        for (int i = 0; i < colliders.Count; i++)
        {
            //如果碰撞到了 计算当前box可以移动最远距离
            if (Physics2D.isCollision(dyBox, colliders[i]))
            {
                if (dir.x != 0)
                {
                    len = Math.Abs(box.centerX - colliders[i].centerX) - (box.width + colliders[i].width) / 2;
                }
                else
                {
                    len = Math.Abs(box.centerY + box.height / 2 - colliders[i].centerY) - (box.height + colliders[i].height) / 2;
                }
                len -= 0.01f;
                return true;
            }
        }
        return false;
    }
}
