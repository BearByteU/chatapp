using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class MessageDto
    {
        public string MessageDescription { get; set; }
        public int UserTo { get; set; }
        public int UserFrom { get; set; }
    }
}
