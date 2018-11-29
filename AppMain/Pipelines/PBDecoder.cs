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
        readonly short HEAD_FLAG = 0x71ab;
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            //包头有了
            if (input.ReadableBytes >= HEAD_LENG)
            {
                short flag = input.ReadShortLE();
                if (flag != HEAD_FLAG)
                {
                    //假客户端 断开客户端 todo
                }
                else
                {
                    short len = input.ReadShortLE();
                    len -= HEAD_LENG;
                    short cmd = input.ReadShortLE();
                    long playerId = input.ReadLongLE();
                    int encryptId = input.ReadIntLE();
                    PBMessage pb = new PBMessage();
                    pb.code = cmd;
                    pb.playerId = playerId;
                    if (input.ReadableBytes >= len)
                    {
                        byte[] data = new byte[len];
                        input.ReadBytes(data);
                        pb.data = data;
                        output.Add(pb);
                    }
                }
            }
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
