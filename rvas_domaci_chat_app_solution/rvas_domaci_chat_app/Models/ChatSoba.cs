using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rvas_domaci_chat_app.Models
{
    public class ChatSoba
    {
        public int id { get; set; }
        public string naziv_sobe { get; set; }
        public string mail_kretora { get; set; }

        public ChatSoba()
        {

        }
    }
}
