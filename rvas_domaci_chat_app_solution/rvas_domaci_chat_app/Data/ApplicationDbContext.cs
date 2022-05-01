using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using rvas_domaci_chat_app.Models;

namespace rvas_domaci_chat_app.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<rvas_domaci_chat_app.Models.ChatSoba> ChatSoba { get; set; }
        public DbSet<rvas_domaci_chat_app.Models.Poruka> Poruka { get; set; }
    }
}
