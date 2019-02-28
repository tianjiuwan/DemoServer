using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;

namespace AppMain
{
    using pb;

    public class PlayerCreateControl : BaseControl
    {
        private int uidIndex = 900010000;
        private string userName = "coco";

        public override void onDispose()
        {

        }

        //请求创建玩家
        public override void onExecute(IChannelHandlerContext ctx, PBMessage pbs)
        {
            Console.WriteLine("PlayerCreateControl msg.playerId " + pbs.playerId);
            pb.GetPlayerInfoMsg msg = ProtobufSerializer.DeSerialize<pb.GetPlayerInfoMsg>(pbs.data);
            //返回
            pb.PlayerInfoMsg rtn = new pb.PlayerInfoMsg();
            uidIndex++;
            string name = userName + uidIndex;
            rtn.playerId = uidIndex;
            rtn.name = name;
            rtn.resName = @"AssetBundle\Prefabs\model\rolemine\role_mine";
            //pos
            pb.PBVector2 pos = new pb.PBVector2();
            pos.x = -3 * 1000;
            pos.y = -3 * 1000;
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
            rtn.roleType = 2;
            //hit box
            PBVector2 hitBox = new PBVector2();
            hitBox.x = (long)(2.5f * 1000);
            hitBox.y = (long)(3.5f * 1000);
            rtn.hitBox = hitBox;
            //通知客户端创建实体
            createRole(rtn);
            send(ctx, 102, rtn);
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
    }

}
