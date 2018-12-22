using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;
using DotNetty.Buffers;

namespace AppMain
{
    public class EntityMgr : Singleton<EntityMgr>
    {
        private Dictionary<int, BaseEntity> rolePool = null;

        protected override void initialize()
        {
            rolePool = new Dictionary<int, BaseEntity>();
        }

        public void create(int roleId, RoleType roleType, IChannel channel = null)
        {
            if (!rolePool.ContainsKey(roleId))
            {
                BaseEntity be = EntityFactory.get(roleId, roleType, channel);
                rolePool.Add(roleId, be);
            }
        }
    }
}
