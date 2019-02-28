using System;
using System.Collections.Generic;

public class BaseState
{
    public FSM_Flag Flag
    {
        get; set;
    }
    public BaseEntity Agent
    {
        get; set;
    }

    public virtual bool allow(FSM_Flag flag) {
        return true;
    }

    public virtual void onEnter(FSMArgs args = null)
    {

    }

    public virtual void onTick()
    {

    }

    public virtual void onExit()
    {

    }

}

