using Pool;
using TwlPhy;

/// <summary>
/// 伤害包处理
/// </summary>
public class HartUtils
{
    //测试一下
    public static void attack(BaseEntity caster)
    {
        var ier = EntityMgr.getPool().GetEnumerator();
        long uid = caster.UID;
        while (ier.MoveNext())
        {
            if (ier.Current.Value.UID != uid)
            {
                FSMArgs args = Pool.PoolMgr.Instance.getData<FSMArgs>();
                HitData data = Pool.PoolMgr.Instance.getData<HitData>();
                Vector2 dir = caster.DyBox.centerX - ier.Current.Value.DyBox.centerY > 0 ? Vector2.left : Vector2.right;
                data.attackDir = dir;
                data.duration = 4;
                data.startFrame = FrameMgr.NowFrame;
                data.repelValue = 0.5f;
                args.hitData = data;
                ier.Current.Value.transFsm(FSM_Flag.StandHit, args);
            }
        }
    }

    /// <summary>
    /// 测试一下攻击碰撞盒检测
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="atkData"></param>
    public static void attack(BaseEntity caster, AttackData atkData)
    {
        var ier = EntityMgr.getPool().GetEnumerator();
        long uid = caster.UID;
        while (ier.MoveNext())
        {
            if (ier.Current.Value.UID != uid)
            {
                if (Physics3D.isCollision(atkData.hitBox, ier.Current.Value.HitBox))
                {
                    FSMArgs args = Pool.PoolMgr.Instance.getData<FSMArgs>();

                    HitData data = Pool.PoolMgr.Instance.getData<HitData>();
                    Vector2 dir = caster.DyBox.centerX - ier.Current.Value.DyBox.centerX > 0 ? Vector2.left : Vector2.right;
                    data.attackDir = dir;
                    data.duration = 4;
                    data.startFrame = FrameMgr.NowFrame;
                    data.repelValue = 0.5f;
                    args.hitData = data;

                    ier.Current.Value.transFsm(FSM_Flag.StandHit, args);
                }
            }
        }
    }


}

