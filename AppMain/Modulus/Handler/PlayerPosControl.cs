using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;

namespace AppMain
{
    public class PlayerPosControl : BaseControl
    {
        public override void onDispose()
        {

        }

        public override void onExecute(IChannelHandlerContext ctx, PBMessage pbs)
        {
            //pb.PlayerSnapShootMsg sanp = ProtobufSerializer.DeSerialize<pb.PlayerSnapShootMsg>(pbs.data);
            //Console.WriteLine("服务器PlayerSnapControl接受到玩家消息 username " + sanp.username);
            ////处理逻辑
            ////返回消息给服务器
            //pb.PlayerSnapShootMsg rtn = new pb.PlayerSnapShootMsg();
            //rtn.playerId = 1000001;
            //rtn.username = "server-cocotang";
            ////单独回
            ////send(ctx, rtn);
            ////广播
            //boradcast(rtn);
        }
    }
}
