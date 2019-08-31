using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Agent.Handle
{
    public class Context
    {
        public HandleType HandleType { get; set; }
        public object Data { get; set; }
    }
}
