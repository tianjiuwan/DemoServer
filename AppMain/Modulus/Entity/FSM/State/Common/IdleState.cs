using System;
using System.Collections.Generic;

public class IdleState : BaseState
{
    public override void onEnter(FSMArgs args = null)
    {
        this.Agent.playAnim("idle", true);
    }

}
