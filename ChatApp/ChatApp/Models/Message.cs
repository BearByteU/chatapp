using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Message
    {
        public Message()
        {

        }
        public int Id { get; set; }
        public string MessageDescription { get; set; }
        [ForeignKey("UserTo")]
        public int UserTo { get; set; }
        public User Uesr { get; set; }
        [ForeignKey("UserFrom")]
        public int UserFrom { get; set; }
        public User User { get; set; }

    }
}
