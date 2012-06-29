using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;

namespace MessageServer
{
    public class User
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }
        public ICService Callback { get; set; }
    }
}
