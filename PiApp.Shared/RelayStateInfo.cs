using System;
using System.Collections.Generic;
using System.Text;

namespace PiApp.Shared
{
    public class RelayStateInfo
    {
        public RelayStateInfo()
        {
        }

        public RelayStateInfo(int id, bool state)
        {
            Id = id;
            State = state;
        }

        public int Id { get; set; }
        public bool State { get; set; }
    }
}
