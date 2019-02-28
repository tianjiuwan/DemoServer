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

        public void send(IChannelHandlerContext ctx, short cmd, Object obj, long playerId = 0)
        {
            byte[] data = ProtobufSerializer.Serialize(obj);
            int len = data.Length;
            IByteBuffer buffer = Unpooled.Buffer();
            buffer.WriteShort(HEAD_FIX);
            buffer.WriteShort((short)(HEAD_LENG + len));
            buffer.WriteShort(cmd);
            buffer.WriteLong(playerId);
            buffer.WriteInt(0);
            buffer.WriteBytes(data);
            ctx.WriteAndFlushAsync(buffer);
        }

        public void boradcast(Object obj,short cmd, long playerId)
        {
            byte[] data = ProtobufSerializer.Serialize(obj);
            int len = data.Length;
            IByteBuffer buffer = Unpooled.Buffer();
            buffer.WriteShort(HEAD_FIX);
            buffer.WriteShort((short)(HEAD_LENG + len));
            buffer.WriteShort(cmd);
            buffer.WriteLong(playerId);
            buffer.WriteInt(0);
            buffer.WriteBytes(data);
            ChannelGroup.Instance.boradcast(buffer);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public abstract void onDispose();
    }
}
