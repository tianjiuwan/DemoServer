using System;
using System.Collections.Generic;
using TwlPhy;

public class PhyRunState : BaseState
{
    private float interval = 1.0f / 30;
    Vector2 dir = new Vector2(0, 0);

    public override void onEnter(FSMArgs args = null)
    {
        this.Agent.playAnim("run", true);
    }

    public override void onTick()
    {
        Agent.updateRenderOrder();
        float dis = 0;
        Agent.moveVec = Agent.moveDir * Agent.speed * interval;
        float dst = Agent.moveDir.x != 0 ? Agent.moveVec.x : Agent.moveVec.y;
        dir.x = Agent.moveDir.x;
        dir.y = Agent.moveDir.y;
        canMove(dir, Math.Abs(dst), ref dis);
        Agent.doMove(Agent.moveDir * dis);
        syncMove();
    }

    private void syncMove()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>方向
    /// <param name="dstDis"></param>目标移动距离
    /// <param name="validDis"></param>有效移动距离
    /// <returns></returns>
    public bool canMove(Vector2 dir, float dst, ref float validDis)
    {
        return Physics2D.isCollision(Agent.DyBox, dir, dst, ref validDis);
    }

    public override bool allow(FSM_Flag flag)
    {
        return flag != this.Flag;
    }

}

