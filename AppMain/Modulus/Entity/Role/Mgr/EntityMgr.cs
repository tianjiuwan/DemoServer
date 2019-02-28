using System;
using System.Collections.Generic;

public class EntityMgr : Singleton<EntityMgr>
{
    private Dictionary<long, BaseEntity> rolePool = null;
    public long mainRoleId = 0;

    protected override void initialize()
    {
        rolePool = new Dictionary<long, BaseEntity>();
    }

    private T get<T>(long uid) where T : BaseEntity, new()
    {
        if (rolePool.ContainsKey(uid))
            return rolePool[uid] as T;
        return null;
    }

    private T create<T>(RoleData data) where T : BaseEntity, new()
    {
        if (data == null) return null;
        if (rolePool.ContainsKey(data.uid)) return null;
        T role = new T();
        role.RoleData = data;
        rolePool.Add(data.uid, role);
        if (mainRoleId <= 0)
            mainRoleId = data.uid;
        return role;
    }

    public void tick()
    {
        var ier = rolePool.GetEnumerator();
        while (ier.MoveNext())
        {
            ier.Current.Value.Tick();
        }
        ier.Dispose();
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static T createRole<T>(RoleData data) where T : BaseEntity, new()
    {
        return Instance.create<T>(data);
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uid"></param>
    /// <returns></returns>
    public static T getRole<T>(long uid) where T : BaseEntity, new()
    {
        return Instance.get<T>(uid);
    }

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <returns></returns>
    public static Dictionary<long, BaseEntity> getPool()
    {
        return Instance.rolePool;
    }
}

