using System;

/// <summary>
/// 逻辑帧 先做单机
/// 1000毫秒 / 30 = 33
/// Update每帧检查客户端当前时间 计算出当前走到了哪一帧
/// 
/// </summary>
public class FrameMgr : Singleton<FrameMgr>
{

    public static int NowFrame
    {
        get
        {
            return Instance.nowFrame;
        }
    }

    private double startTime;
    private double nowTime;
    private float interval;
    private int nowFrame = 0;

    protected override void initialize()
    {
        interval = 1000 / 30;
        startTime = TimerUtils.getMillTimer();
    }

    public void tick()
    {
        nowTime = TimerUtils.getMillTimer();
        double sub = nowTime - startTime;
        //  22060  22000  = 60ms
        nowFrame = (int)Math.Floor((float)sub / interval);
        EntityMgr.Instance.tick();
    }

}
