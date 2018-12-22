using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;

namespace AppMain
{
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
            //返回
            pb.PlayerInfoMsg rtn = new pb.PlayerInfoMsg();
            uidIndex++;
            string name = userName + uidIndex;
            rtn.playerId = uidIndex;
            rtn.name = name;
            pb.PBVector3 pos = new pb.PBVector3();
            pos.x = 0;
            pos.y = 1;
            pos.z = 0;
            rtn.pos = pos;
            //创建实体 todo
            send(ctx, Cmd.playerInfoMsgResp, rtn);
        }
    }
}
