using System;
using System.Collections.Generic;

public class ShootState : BaseState
{
    public override void onEnter(FSMArgs args = null)
    {
        Agent.modifyCompleteEvent(true);
        Agent.playAnim("shoot");
    }

}

