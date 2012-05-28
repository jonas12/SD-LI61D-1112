using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CommonInterface
{
    public class MethodCallEventArgs : EventArgs
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
