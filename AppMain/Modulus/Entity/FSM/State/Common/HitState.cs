using System;
using System.Collections.Generic;

/// <summary>
/// 实体被击(参考DNF)
/// 1:站立受击 
/// ｛
///    受击类型: 自适应 
///    
/// ｝击退 则击退 浮空则浮空
/// 2:浮空受击 受击类型 
/// </summary>
public class HitState : BaseState
{
    private HitData hitData;

    public override void onEnter(FSMArgs args = null)
    {
        hitData = args.hitData;
        if (hitData == null)
        {
            Agent.transFsm(FSM_Flag.Idle, null, true);
        }
        //强转到站立受击        
        Agent.transFsm(FSM_Flag.StandHit, args);
    }

    public override void onTick()
    {
        if (FrameMgr.NowFrame > hitData.endFrame)
        {
            Agent.transFsm(FSM_Flag.Idle, null, true);
        }
    }

    public override void onExit()
    {
        hitData = null;
    }
}

