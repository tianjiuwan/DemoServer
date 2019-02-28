using System;
using System.Collections.Generic;
using TwlPhy;

public class StandHitState : BaseState
{
    private HitData hitData;

    public override void onEnter(FSMArgs args = null)
    {
        hitData = args.hitData;
        int animIndex = hitData.hitAnimIndex;
        string animName = "hit";
        Agent.playAnim(animName);
        //回收一下
        Pool.PoolMgr.Instance.recyleData(args);
    }

    public override void onTick()
    {
        if (FrameMgr.NowFrame > hitData.endFrame)
        {
            Agent.transFsm(FSM_Flag.Idle, null, true);
            return;
        }
        //每帧击退
        float dst = hitData.repelFrame;
        float validDis = 0;
        Physics2D.isCollision(Agent.DyBox, hitData.attackDir, dst, ref validDis);
        Agent.doMove(hitData.attackDir * validDis);
    }

    public override void onExit()
    {
        //回收一下
        Pool.PoolMgr.Instance.recyleData(hitData);
        hitData = null;
    }
}

