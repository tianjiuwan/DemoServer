namespace AppMain
{
    using System;
    using DotNetty.Transport.Channels;
    using DotNetty.Buffers;
    using System.Collections.Generic;

    /// <summary>
    /// 管理类
    /// </summary>
    public class TestServerHandler : SimpleChannelInboundHandler<object>
    {
        private Dictionary<int, BaseControl> handlerMap = new Dictionary<int, BaseControl>();

        /// <summary>
        /// 进入的消息
        /// 协议号一定要前后端对应 如果没有对应的handler则不会处理消息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        protected override void ChannelRead0(IChannelHandlerContext context, object message)
        {            
            PBMessage pbs = message as PBMessage;
            int cmd = pbs.cmd;
            Console.WriteLine("服务器接受到客户端消息  cmd  ---> " + cmd);
            if (handlerMap.ContainsKey(cmd)) {
                handlerMap[cmd].onExecute(context, pbs);
            }
        }

        //public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        /// <summary>
        /// 初始化调用
        /// </summary>
        /// <param name="ctx"></param>
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            Console.WriteLine("<<<<----客户端链接 " );
            ChannelGroup.Instance.add(ctx.Channel);
            handlerMap.Add(Cmd.playerInfoMsgReq, new PlayerCreateControl());
            handlerMap.Add(Cmd.playerPosReq, new PlayerPosControl());
        }

        public override void HandlerRemoved(IChannelHandlerContext context)
        {
            base.HandlerRemoved(context);
            ChannelGroup.Instance.remove(context.Channel);
            Console.WriteLine("客户端断开连接--->>>> ");
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine("{0}", e.ToString());
            ChannelGroup.Instance.remove(ctx.Channel);
            ctx.CloseAsync();
        }
    }
}
