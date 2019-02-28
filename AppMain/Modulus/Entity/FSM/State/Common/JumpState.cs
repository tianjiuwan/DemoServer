using TwlPhy;

public class JumpState : BaseState
{
    private float jumpSpeed = 5f;
    private float interval = 1f / 30;
    //跳跃还在移动Y轴
    //记录一个Z轴位置 当作跳跃高度
    //获取玩家真实位置 需要判断 玩家是否在浮空中
    /*jump动画循环播放
     * jump时间由角色可以跳跃到的高度算出
     * 时间结束转到下落状态 并播放下落动画 到达地面
     */
    public override void onEnter(FSMArgs args = null)
    {
        Agent.modifyCompleteEvent(false);
        this.Agent.playAnim("jump", true);
    }

    public override void onTick()
    {
        float add = jumpSpeed * interval;
        bool isArrive = false;
        if (Agent.Height + add >= Agent.jumpHeight)
        {
            add = Agent.jumpHeight - Agent.Height;
            isArrive = true;
        }
        Agent.Height += add;
        //Agent.Trans.Translate(Vector2.up * add);
        Agent.doMove(Vector2.up * add);
        if (isArrive)
        {
            Agent.transFsm(FSM_Flag.Fall);
        }
    }

    public override bool allow(FSM_Flag flag)
    {
        return flag == FSM_Flag.Fall;
    }
}

