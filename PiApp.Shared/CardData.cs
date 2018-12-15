using System;
using System.Collections.Generic;
using System.Text;

namespace PiApp.Shared
{
    public class CardData
    {
        public CardData(byte[] uid)
        {
            UID = uid;
        }

        public byte[] UID { get; set; }
    }
}
