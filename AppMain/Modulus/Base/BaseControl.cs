namespace AppMain
{
    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;
    using System;
    using System.Collections.Generic;

    public abstract class BaseControl
    {
        short HEAD_FIX = 0x71ab;
        short HEAD_LENG = 18;
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="pbs"></param>
        public abstract void onExecute(IChannelHandlerContext ctx, PBMessage pbs);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="obj"></param>
        public void send(IChannelHandlerContext ctx, Object obj)
        {
            byte[] data = ProtobufSerializer.Serialize(obj);
            int len = data.Length;
            IByteBuffer buffer = Unpooled.Buffer();
            buffer.WriteShort(HEAD_FIX);
            buffer.WriteShort((short)(HEAD_LENG + len));
            buffer.WriteShort(12001);
            buffer.WriteLong(90000001001);
            buffer.WriteInt(0);
            buffer.WriteBytes(data);
            //马上返回一条消息给客户端
            Console.WriteLine("服务器返回一条消息给客户端------------------------------- ");
            ctx.WriteAndFlushAsync(buffer);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public abstract void onDispose();
    }
}
