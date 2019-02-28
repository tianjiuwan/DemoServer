using System;
using System.Collections.Generic;


public enum Attack_Type
{
    None,
    Repel,//自适应  站立击退,浮空击飞
    OnlyStrike,//击飞 站立击飞,浮空击飞
    OnlyRepel,//击退 站立击退,浮空无位移作用
    Rigidity,//僵直
}

public class FsmEnum
{
}

