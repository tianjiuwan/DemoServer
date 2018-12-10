using System;
using System.Collections.Generic;

/// <summary>
/// 格子
/// 
/// </summary>
public class Grid
{
    /*思路
    *任意地图看成矩形 矩形长L 宽W 中心C
    * 每一个格子  长GL 宽GW
    */
    public int index;
    public List<BaseEntity> entityPool = null;

    public Grid(int index) {
        this.index = index;
        entityPool = new List<BaseEntity>();
    }

    public void addEntity(BaseEntity entity) {
        entityPool.Add(entity);
    }

    public void removeEntity(BaseEntity entity) {
        if (entityPool.Contains(entity)) {
            entityPool.Remove(entity);
        }
    }

}

