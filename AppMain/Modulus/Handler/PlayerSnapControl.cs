using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace AppMain
{
    public class PlayerSnapControl : BaseControl
    {
        public override void onDispose()
        {
            
        }

        public override void onExecute(IChannelHandlerContext ctx, PBMessage pbs)
        {

        }
    }
}
