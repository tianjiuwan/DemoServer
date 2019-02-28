using System;

/// <summary>
/// 角色面向
/// </summary>
public enum LookFlag
{
    None,
    Left,
    Right,
}

/// <summary>
/// 状态机枚举
/// </summary>
public enum FSM_Flag
{
    None = 0,
    Idle,
    Run,
    Walk,
    Jump,
    Fall,
    Shoot,
    Skill,
    Hit,
    StandHit,
    AirHit,
}


/// <summary>
/// 实体类型
/// </summary>
public enum Role_Type
{
    None = 0,
    NetPlayer,
    MainPlayer,
    Monster,
    Npc,
}

/// <summary>
/// 实体加载状态
/// </summary>
public enum E_EntityLoadState
{
    Waiting,
    Loading,
    Finish,
    Invaild,
}

public class EntityDefine
{
}

