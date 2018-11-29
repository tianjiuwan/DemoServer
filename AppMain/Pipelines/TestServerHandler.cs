namespace AppMain
{
    using System;
    using DotNetty.Transport.Channels;
    using DotNetty.Buffers;
    using System.Text;

    public class TestServerHandler : SimpleChannelInboundHandler<object>
    {
        /// <summary>
        /// 进入的消息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        protected override void ChannelRead0(IChannelHandlerContext context, object message)
        {
            PBMessage pbs = message as PBMessage;
            //string val = Encoding.Default.GetString(pb.data);
            //todo
            //业务handler在这里管理 存一张map 通过cmd派发给对应handler处理msg
            pb.PlayerSnapShootMsg sanp = ProtobufSerializer.DeSerialize<pb.PlayerSnapShootMsg>(pbs.data);
            Console.WriteLine("接受到客户端消息------------------------------->TestServerHandler  username " + sanp.username);

            //马上返回一条消息给客户端
            context.WriteAsync(message);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        /// <summary>
        /// 初始化调用
        /// </summary>
        /// <param name="ctx"></param>
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            Console.WriteLine("有客户端链接-------------------------------> ");
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine("{0}", e.ToString());
            ctx.CloseAsync();
        }
    }
}
