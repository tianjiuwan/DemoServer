﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMain
{
    public class PBMessage
    {
        public long playerId;
        public byte[] data;
        public int cmd;
        public int len;
    }
}
