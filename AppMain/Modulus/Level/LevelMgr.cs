using System;
using System.Collections.Generic;
using TwlPhy;

public class LevelMgr : Singleton<LevelMgr>
{
    private BaseLevel nowLevel = null;
    public BaseLevel NowLevel
    {
        get
        {
            return nowLevel;
        }
    }

    protected override void initialize()
    {
        nowLevel = new BaseLevel();
    }

    public void init()
    {
        //测试数据
        Box2D box1 = new Box2D(2, 0, 22, 1);
        Box2D box2 = new Box2D(2, -5.5f, 22, 1);
        Box2D box3 = new Box2D(-7, 0, 1, 20);
        Box2D box4 = new Box2D(8f, 0, 1, 20);
        Box2D box5 = new Box2D(-0.18f, -3, 1, 1);
        NowLevel.addBox(box1);
        NowLevel.addBox(box2);
        NowLevel.addBox(box3);
        NowLevel.addBox(box4);
        NowLevel.addBox(box5);
    }

}

