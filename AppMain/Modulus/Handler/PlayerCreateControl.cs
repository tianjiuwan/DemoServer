using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;

namespace AppMain
{
    using pb;

    public class PlayerCreateControl : BaseControl
    {
        private string userName = "coco";

        public override void onDispose()
        {

        }

        //请求创建玩家
        public override void onExecute(IChannelHandlerContext ctx, PBMessage pbs)
        {
            pb.GetPlayerInfoMsg msg = ProtobufSerializer.DeSerialize<pb.GetPlayerInfoMsg>(pbs.data);
            //返回
            long uid = MathUtils.UniqueID;
            string name = userName;
            PlayerInfoMsg rtn = getInfoMsg(uid, name, 2);
            Console.WriteLine("PlayerCreateControl msg.playerId " + rtn.playerId);
            //服务器创建实体
            createRole(rtn);
            //通知客户端创建实体
            send(ctx, 102, rtn, 0);
            //
            boardSuffRole(rtn);
            //
            boardPreRole(rtn.playerId, ctx);
        }

        //通知当前已经连接的玩家
        private void boardSuffRole(PlayerInfoMsg rtn)
        {
            rtn.roleType = 3;
            boradcast(rtn, 102, 0);
        }

        //通知当前玩家之前连接的玩家
        private void boardPreRole(long playerId, IChannelHandlerContext ctx)
        {
            Dictionary<long, BaseEntity> pool = EntityMgr.getPool();
            var ier = pool.GetEnumerator();
            while (ier.MoveNext())
            {
                BaseEntity role = ier.Current.Value;
                if (role.UID != playerId)
                {
                    PlayerInfoMsg rtn = getInfoMsg(role.UID, role.RoleData.nickName, 3, role);
                    send(ctx, 102, rtn);
                }
            }
        }

        private void createRole(PlayerInfoMsg msg)
        {
            if (EntityMgr.getRole<BaseEntity>(msg.playerId) != null)
            {
                return;
            }
            RoleData roleData = Pool.PoolMgr.Instance.getData<RoleData>();
            roleData.uid = msg.playerId;
            roleData.nickName = msg.name;
            roleData.resName = msg.resName;
            roleData.pos = new TwlPhy.Vector2(msg.pos.x * 0.001f, msg.pos.y * 0.001f);
            roleData.moveBox = new TwlPhy.Vector2(msg.moveBox.x * 0.001f, msg.moveBox.y * 0.001f);
            roleData.height = msg.height * 0.001f;
            roleData.speed = msg.speed * 0.001f;
            roleData.jumpSpeed = msg.jumpSpeed * 0.001f;
            roleData.scale = new TwlPhy.Vector3(msg.scale.x * 0.001f, msg.scale.y * 0.001f, msg.scale.z * 0.001f);
            roleData.roleType = (Role_Type)(msg.roleType);
            roleData.hitBox = new TwlPhy.Vector2(msg.hitBox.x * 0.001f, msg.hitBox.y * 0.001f);
            EntityMgr.createRole<NetPlayer>(roleData);
        }

        private PlayerInfoMsg getInfoMsg(long id, string name, int roleType, BaseEntity role = null)
        {
            pb.PlayerInfoMsg rtn = new pb.PlayerInfoMsg();
            rtn.playerId = id;
            rtn.name = name;
            rtn.resName = @"AssetBundle\Prefabs\model\rolemine\role_mine";
            //pos
            pb.PBVector2 pos = new pb.PBVector2();
            pos.x = role != null ? (long)(role.DyBox.centerX * 1000) : -3 * 1000;
            pos.y = role != null ? (long)(role.DyBox.centerY * 1000) : -3 * 1000;
            rtn.pos = pos;
            //move box
            PBVector2 moveBox = new PBVector2();
            moveBox.x = (long)(2 * 1000);
            moveBox.y = (long)(0.4f * 1000);
            rtn.moveBox = moveBox;
            //height
            rtn.height = 0;
            //speed
            rtn.speed = (long)(3 * 1000);
            //jumpSpeed
            rtn.jumpSpeed = (long)(4 * 1000);
            //缩放
            PBVector3 scale = new PBVector3();
            scale.x = (long)(0.5f * 1000);
            scale.y = (long)(0.5f * 1000);
            scale.z = (long)(1 * 1000);
            rtn.scale = scale;
            //role Type
            rtn.roleType = roleType;
            //hit box
            PBVector2 hitBox = new PBVector2();
            hitBox.x = (long)(2.5f * 1000);
            hitBox.y = (long)(3.5f * 1000);
            rtn.hitBox = hitBox;
            return rtn;
        }
    }

}
