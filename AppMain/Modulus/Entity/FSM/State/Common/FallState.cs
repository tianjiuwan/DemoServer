using TwlPhy;

public class FallState : BaseState
{
    //掉落加速度
    private float addSpeed = 0.01f;
    private float fallSpeed = 2f;
    private float interval = 1f / 30;

    public override void onEnter(FSMArgs args = null)
    {
        Agent.modifyCompleteEvent(false);
        this.Agent.playAnim("jump old", true);
    }

    public override void onTick()
    {
        fallSpeed += addSpeed;
        float add = fallSpeed * interval;
        bool isArrive = false;
        if (Agent.Height - add <= 0)
        {
            add = Agent.Height;
            isArrive = true;
        }
        Agent.Height -= add;
        //Agent.Trans.Translate(Vector2.up * -1 * add);
        Agent.doMove(Vector2.up * -1 * add);
        if (isArrive)
        {
            Agent.transFsm(FSM_Flag.Idle, null, true);
        }
    }

    public override void onExit()
    {
        fallSpeed = 2f;
    }

    public override bool allow(FSM_Flag flag)
    {
        return false;
    }
}