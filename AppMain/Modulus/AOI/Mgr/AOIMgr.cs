using System;
using System.Collections.Generic;

/// <summary>
/// 视野管理 先做个单张地图
/// </summary>
public class AOIMgr:Singleton<AOIMgr>
{
    //定义地图相关
    private int mapL;
    private int mapW;
    private Vector2 mapC;
    private int gridL;
    private int gridW;

    //key = grid.index val = grid
    public Dictionary<int, Grid> map = null;

    protected override void initialize()
    {
        map = new Dictionary<int, Grid>();
    }

    /// <summary>
    /// 玩家移动
    /// 计算当前的格子 周围的格子
    /// 移除旧格子 
    /// </summary>
    /// <param name="entity"></param>
    public void onMove(BaseEntity entity) {

    }

    /// <summary>
    /// 计算pos所在的格子
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private int getIndex(Vector3 pos) {
        return 0;
    }

    private void getRound() {

    }
}
