using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using rvas_domaci_chat_app.Data;
using rvas_domaci_chat_app.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;

namespace poruke_namespace
{
    public class MessageHub : Hub
    {
        [Authorize]
        public Task SendMessageToAll(string message, string id_sobe)
        {
            string user = Context.User.Identity.Name;
            if ( message == "konektovanje_u_sobu")
            {
                Groups.AddToGroupAsync(Context.ConnectionId, id_sobe.ToString());
                return Clients.Group(id_sobe.ToString()).SendAsync("ReceiveMessage", "Korisnik je konektovan", user.ToString());
            }
            Poruka nova_poruka = new Poruka();
            nova_poruka.poruku_poslao = user .ToString();
            nova_poruka.id_sobe = int.Parse(id_sobe);
            nova_poruka.text_poruke = message;
            nova_poruka.SaveDetails();
            return Clients.Group(id_sobe.ToString()).SendAsync("ReceiveMessage", message, user.ToString());
        }
    }
}