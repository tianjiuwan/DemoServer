namespace AppMain
{
    using System;
    using DotNetty.Transport.Channels;
    using DotNetty.Buffers;
    using System.Collections.Generic;


    public class TestServerHandler : SimpleChannelInboundHandler<object>
    {
        private Dictionary<int, BaseControl> handlerMap = new Dictionary<int, BaseControl>();

        /// <summary>
        /// 进入的消息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        protected override void ChannelRead0(IChannelHandlerContext context, object message)
        {
            PBMessage pbs = message as PBMessage;
            int cmd = pbs.cmd;
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
            Console.WriteLine("有客户端链接-------------------------------> ");
            handlerMap.Add(Cmd.playerSanp, new PlayerSnapControl());
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine("{0}", e.ToString());
            ctx.CloseAsync();
        }
    }
}
