using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rvas_domaci_chat_app.Models
{
    public class Poruka
    {
        public int id { get; set; }
        public string poruku_poslao{ get; set; }
        public string text_poruke{ get; set; }
        public int id_sobe { get; set; }

        public Poruka()
        {

        }
    }
}
