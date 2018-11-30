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

            // short HEAD_FIX = 0x71ab;
            //short HEAD_LENG = 18;
            //for (int i = 0; i < 100; i++)
            //{
            //    pb.PlayerSnapShootMsg msg = new pb.PlayerSnapShootMsg();
            //    msg.playerId = 80000060;
            //    msg.username = "cocotang"+i;
            //    byte[] data = ProtobufSerializer.Serialize<pb.PlayerSnapShootMsg>(msg);
            //    int len = data.Length;
            //    IByteBuffer buffer = Unpooled.Buffer();
            //    buffer.WriteShort(HEAD_FIX);
            //    buffer.WriteShort((short)(HEAD_LENG + len));
            //    buffer.WriteShort(12001);
            //    buffer.WriteLong(90000001001);
            //    buffer.WriteInt(0);
            //    buffer.WriteBytes(data);
            //    //马上返回一条消息给客户端
            //    Console.WriteLine("马上返回一条消息给客户端------------------------------->index " + i);
            //    context.WriteAndFlushAsync(buffer);
            //}
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
