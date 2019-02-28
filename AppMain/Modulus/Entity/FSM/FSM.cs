using System;
using System.Collections.Generic;

/// <summary>
/// 状态机管理器
/// 每个状态的时间长度是具体的逻辑帧
/// 每次tick 走逻辑帧
/// 逻辑帧到达退出状态
/// todo
/// </summary>
public class FSM
{
    public FSM(BaseEntity agent)
    {
        this.agent = agent;
    }

    private Dictionary<FSM_Flag, BaseState> pool = new Dictionary<FSM_Flag, BaseState>();
    private BaseState currState = null;
    private BaseEntity agent;
    public FSM_Flag Flag
    {
        get
        {
            if (currState != null)
                return currState.Flag;
            return FSM_Flag.None;
        }
    }

    public virtual void tick()
    {
        if (currState != null)
        {
            currState.onTick();
        }
    }

    /// <summary>
    /// 转换状态
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="args"></param>
    public bool transFsm(FSM_Flag flag, FSMArgs args = null, bool forceTrans = false)
    {
        bool isSuccess = false;
        if (pool.ContainsKey(flag))
        {
            if (currState == null || forceTrans || currState.allow(flag))
            {
                if (currState != null)
                    currState.onExit();
                currState = pool[flag];
                currState.onEnter(args);
                isSuccess = true;
            }
        }
        return isSuccess;
    }

    /// <summary>
    /// 添加一个状态
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="state"></param>
    public void addState(FSM_Flag flag, BaseState state)
    {
        state.Flag = flag;
        state.Agent = this.agent;
        if (!pool.ContainsKey(flag))
        {
            pool.Add(flag, state);
        }
    }
}

