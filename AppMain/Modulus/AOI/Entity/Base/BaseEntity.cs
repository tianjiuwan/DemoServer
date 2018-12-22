using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;
using DotNetty.Buffers;
using AppMain;

public class BaseEntity
{
    public BaseEntity(int id, RoleType roleType) {
        this.id = id;
        this.roleType = roleType;
    }
    public BaseEntity(int id, RoleType roleType, IChannel channel)
    {
        this.id = id;
        this.roleType = roleType;
        this.channel = channel;
    }
    //属性
    private int id;
    private RoleType roleType;

    //通信
    private IChannel channel;
    //位置相关
    public Vector3 position;
    public int gridIndex;


    public void sendMsg(IByteBuffer buffer) {
        if (channel != null) {
            channel.WriteAndFlushAsync(buffer);
        }
    }
}
