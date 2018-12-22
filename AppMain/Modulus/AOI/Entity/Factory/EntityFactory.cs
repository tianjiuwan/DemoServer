using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;

namespace AppMain
{
    public enum RoleType {
        None,
        Player,
    }

    public class EntityFactory
    {
        public static BaseEntity get(int id,RoleType roleType, IChannel channel) {
            BaseEntity be = null;
            switch (roleType) {
                case RoleType.Player:
                    be = new BaseEntity(id, roleType, channel);
                    break;
                default:
                    be = new BaseEntity(id, roleType, channel);
                    break;
            }
            return be;
        }
    }
}
