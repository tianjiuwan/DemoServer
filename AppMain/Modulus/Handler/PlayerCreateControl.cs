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
            pb.GetPlayerInfoMsg msg = ProtobufSerializer.DeSerialize<pb.GetPlayerInfoMsg>(pbs.data);
            Console.WriteLine("PlayerCreateControl msg.playerId " + msg.playerId);
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
            send(ctx, 102, rtn);
        }
    }
}
