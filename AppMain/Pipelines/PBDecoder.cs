using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace AppMain
{
    public sealed class PBDecoder : ByteToMessageDecoder
    {
        // todo: maxFrameLength + safe skip + fail-fast option (just like LengthFieldBasedFrameDecoder)
        readonly short HEAD_LENG = 18;
        protected override void Decode(IChannelHandlerContext context, IByteBuffer byteBuffer, List<object> output)
        {
            if (byteBuffer.ReadableBytes >= HEAD_LENG)
            {
                int packLen = getLen(byteBuffer);
                if (byteBuffer.ReadableBytes >= packLen)
                {
                    short flag = byteBuffer.ReadShort();
                    short len = byteBuffer.ReadShort();
                    len -= HEAD_LENG;
                    short cmd = byteBuffer.ReadShort();                    
                    long playerId = byteBuffer.ReadLong();
                    int encryptId = byteBuffer.ReadInt();
                    PBMessage pb = new PBMessage();
                    pb.cmd = cmd;
                    pb.playerId = playerId;
                    byte[] data = new byte[len];
                    byteBuffer.ReadBytes(data);
                    pb.data = data;
                    output.Add(pb);
                }
                else
                {

                }
            }
        }
        private int getLen(IByteBuffer input)
        {
            IByteBuffer buffer = input.Copy();
            short headFlag = buffer.ReadShort();
            short len = buffer.ReadShort();
            return len;
        }

        static int ReadRawVarint32(IByteBuffer buffer)
        {
            Contract.Requires(buffer != null);

            if (!buffer.IsReadable())
            {
                return 0;
            }

            buffer.MarkReaderIndex();
            byte rawByte = buffer.ReadByte();
            if (rawByte < 128)
            {
                return rawByte;
            }

            int result = rawByte & 127;
            if (!buffer.IsReadable())
            {
                buffer.ResetReaderIndex();
                return 0;
            }

            rawByte = buffer.ReadByte();
            if (rawByte < 128)
            {
                result |= rawByte << 7;
            }
            else
            {
                result |= (rawByte & 127) << 7;
                if (!buffer.IsReadable())
                {
                    buffer.ResetReaderIndex();
                    return 0;
                }

                rawByte = buffer.ReadByte();
                if (rawByte < 128)
                {
                    result |= rawByte << 14;
                }
                else
                {
                    result |= (rawByte & 127) << 14;
                    if (!buffer.IsReadable())
                    {
                        buffer.ResetReaderIndex();
                        return 0;
                    }

                    rawByte = buffer.ReadByte();
                    if (rawByte < 128)
                    {
                        result |= rawByte << 21;
                    }
                    else
                    {
                        result |= (rawByte & 127) << 21;
                        if (!buffer.IsReadable())
                        {
                            buffer.ResetReaderIndex();
                            return 0;
                        }

                        rawByte = buffer.ReadByte();
                        result |= rawByte << 28;

                        if (rawByte >= 128)
                        {
                            throw new CorruptedFrameException("Malformed varint.");
                        }
                    }
                }
            }

            return result;
        }
    }
}
