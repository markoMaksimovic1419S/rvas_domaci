using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
        public void SaveDetails()
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-rvas_domaci_chat_app-E9BB2C8D-7456-46E7-ADDA-F524F125E568;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = "INSERT INTO Poruka(poruku_poslao, text_poruke, id_sobe) values ('" + poruku_poslao + "','" + text_poruke + "','" + id_sobe.ToString() + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Poruka()
        {

        }
    }
}
