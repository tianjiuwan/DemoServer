using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;
using pb;
using TwlPhy;

namespace AppMain
{
    public class PlayerPosControl : BaseControl
    {
        //
        private float interval = 1f / 30;

        public override void onDispose()
        {

        }

        public override void onExecute(IChannelHandlerContext ctx, PBMessage pbs)
        {
            pb.SyncPlayerPosReq msg = ProtobufSerializer.DeSerialize<pb.SyncPlayerPosReq>(pbs.data);
            //服务器更新玩家坐标
            updatePos(msg, pbs.playerId);
            //返回消息给客户端 广播
            pb.SyncPlayerPosResp rtn = new pb.SyncPlayerPosResp();
            rtn.playerId = pbs.playerId;
            rtn.dir = msg.dir;
            rtn.speed = msg.speed;
            rtn.utcTime = (long)TimerUtils.getMillTimer();

            boradcast(rtn, 104, pbs.playerId);
        }

        private void updatePos(SyncPlayerPosReq msg, long playerId)
        {
            BaseEntity role = EntityMgr.getRole<BaseEntity>(playerId);
            if (role != null)
            {
                Vector3 pos = new Vector3(msg.pos.x * 0.001f, msg.pos.y * 0.001f, msg.pos.z * 0.001f);
                //Console.WriteLine(string.Format("原始坐标: {0}: {1}", pos.x, pos.y));
                //服务器玩家进行移动
                //role.doMove(new Vector2(msg.dir.x * 0.001f, msg.dir.y * 0.001f));
                //服务器玩家直接踢到预测点
                float add = (long)(TimerUtils.getMillTimer() - msg.utcTime) * 0.001f;
                pos.x += msg.dir.x * 0.001f * msg.speed * 0.001f * interval * add;//当前x+延迟x
                pos.y += msg.dir.y * 0.001f * msg.speed * 0.001f * interval * add;

                role.DyBox.UpdateCenter(pos.x, pos.y);
                role.HitBox.UpdateCenter(pos.x, pos.y);
                Console.WriteLine(string.Format("更新玩家{0}坐标完成,延迟{1},x:{2},y:{3}", playerId, add, pos.x, pos.y));
            }
        }

    }
}
