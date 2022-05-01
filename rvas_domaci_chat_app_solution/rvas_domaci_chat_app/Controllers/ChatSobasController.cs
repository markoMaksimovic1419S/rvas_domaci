using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rvas_domaci_chat_app.Data;
using rvas_domaci_chat_app.Models;

namespace rvas_domaci_chat_app.Controllers
{
    public class ChatSobasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatSobasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChatSobas
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChatSoba.ToListAsync());
        }

        // GET: ChatSobas/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatSoba = await _context.ChatSoba
                .FirstOrDefaultAsync(m => m.id == id);
            if (chatSoba == null)
            {
                return NotFound();
            }

            ViewBag.Message= _context.Poruka.Where(j => j.id_sobe == id).OrderByDescending(s => s.id).ToList();
            return View(chatSoba);
        }

        // GET: ChatSobas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChatSobas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(string naziv_sobe)
        //public string Create(string naziv_sobe)
        {
            //if (ModelState.IsValid)
            // {
            //_context.Add(chatSoba);
            //await _context.SaveChangesAsync();
            //  return RedirectToAction(nameof(Index));
            //}

            var a = HttpContext.User.Identity.Name;
            var the_user = _context.Users.Where(j => j.Email.Contains(a)).ToList()[0];
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatSoba chatSoba = new ChatSoba();
            chatSoba.mail_kretora = the_user.ToString();
            chatSoba.naziv_sobe = naziv_sobe;
            _context.Add(chatSoba);
            _context.SaveChanges();

            //return naziv_sobe + " || " + the_user;

            return RedirectToAction("Details", new { id=chatSoba.id});
        }

        // GET: ChatSobas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatSoba = await _context.ChatSoba.FindAsync(id);
            if (chatSoba == null)
            {
                return NotFound();
            }
            return View(chatSoba);
        }

        // POST: ChatSobas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("id,naziv_sobe,mail_kretora")] ChatSoba chatSoba)
        {
            if (id != chatSoba.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatSoba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatSobaExists(chatSoba.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chatSoba);
        }

        // GET: ChatSobas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatSoba = await _context.ChatSoba
                .FirstOrDefaultAsync(m => m.id == id);
            if (chatSoba == null)
            {
                return NotFound();
            }

            return View(chatSoba);
        }

        // POST: ChatSobas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatSoba = await _context.ChatSoba.FindAsync(id);
            _context.ChatSoba.Remove(chatSoba);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatSobaExists(int id)
        {
            return _context.ChatSoba.Any(e => e.id == id);
        }
    }
}
