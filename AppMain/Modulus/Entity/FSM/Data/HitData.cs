using System;
using Pool;
using TwlPhy;

/// <summary>
/// 伤害数据
/// </summary>
public class HitData : PoolObject
{
    public Vector3 attackDir;
    public Attack_Type attackType;
    public float repelValue;
    public float strikeValue;
    public int duration;//持续帧
    public int startFrame;//起始帧
    public int hitAnimIndex = 0;//0播放hit

    //结束帧
    public int endFrame
    {
        get
        {
            return startFrame + duration;
        }
    }
    //如果是击退 获取每帧击退的距离
    public float repelFrame
    {
        get
        {
            return repelValue / duration;
        }
    }

}
