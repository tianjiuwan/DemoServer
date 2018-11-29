using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMain
{
    class PBMessage
    {
        public long playerId;
        public short protocolId;
        public byte[] data;
        public int key;
        public int len;

        public int getLen()
        {
            return this.len;
        }
        public void setLen(short len)
        {
            this.len = len;
        }

        public void setKey(int key)
        {
            this.key = key;
        }

        public void setBytes(byte[] dt)
        {
            this.data = dt;
        }

        public int code;
        public void setCode(int code)
        {
            this.code = code;
        }
        public void setPlayerId(long id)
        {
            this.playerId = id;
        }
    }
}
