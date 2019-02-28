using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using AppMain;

public class ChannelGroup : Singleton<ChannelGroup>
{
    private List<IChannel> pool = null;

    protected override void initialize()
    {
        pool = new List<IChannel>();
    }

    public void add(IChannel ic)
    {
        if (!pool.Contains(ic))
        {
            pool.Add(ic);
        }
    }
    public void remove(IChannel ic)
    {
        if (pool.Contains(ic))
        {
            pool.Remove(ic);
        }
    }

    public void boradcast(IByteBuffer buffer)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            IByteBuffer msg = buffer.Copy();
            pool[i].WriteAndFlushAsync(msg);
        }
    }
}

