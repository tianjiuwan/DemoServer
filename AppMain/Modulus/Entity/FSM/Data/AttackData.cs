using System;
using Pool;
using TwlPhy;

/// <summary>
/// 攻击数据
/// </summary>
public class AttackData : PoolObject
{
    public Attack_Type attackType;
    public DymicBox3D hitBox;//box
    public Vector3 hitBoxOffset = Vector3.zero;
    public Vector3 hitSize = Vector3.zero;
    public int drawNum = 0;
}