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
    public class PorukasController : Controller
    {
        private readonly ApplicationDbContext _context;

        //POST: Porukas/PorukaSlanje
        [Authorize]
        public JsonResult PorukaSlanje (string poruka_text, int id_sobe)
        {
            var a = HttpContext.User.Identity.Name;
            var the_user = _context.Users.Where(j => j.Email.Contains(a)).ToList()[0];
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Poruka nova_poruka = new Poruka();
            nova_poruka.poruku_poslao = the_user.ToString();
            nova_poruka.id_sobe= id_sobe;
            nova_poruka.text_poruke= poruka_text;
            _context.Add(nova_poruka);
            _context.SaveChanges();
            return Json(new { Status = "ok", za_sobu_id = id_sobe, poruka = poruka_text, poslao= the_user.ToString() });
        }
        public PorukasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Porukas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Poruka.ToListAsync());
        }

        // GET: Porukas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka
                .FirstOrDefaultAsync(m => m.id == id);
            if (poruka == null)
            {
                return NotFound();
            }

            return View(poruka);
        }

        // GET: Porukas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Porukas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,poruku_poslao,text_poruke,id_sobe")] Poruka poruka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poruka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poruka);
        }

        // GET: Porukas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka.FindAsync(id);
            if (poruka == null)
            {
                return NotFound();
            }
            return View(poruka);
        }

        // POST: Porukas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,poruku_poslao,text_poruke,id_sobe")] Poruka poruka)
        {
            if (id != poruka.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poruka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PorukaExists(poruka.id))
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
            return View(poruka);
        }

        // GET: Porukas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka
                .FirstOrDefaultAsync(m => m.id == id);
            if (poruka == null)
            {
                return NotFound();
            }

            return View(poruka);
        }

        // POST: Porukas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poruka = await _context.Poruka.FindAsync(id);
            _context.Poruka.Remove(poruka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PorukaExists(int id)
        {
            return _context.Poruka.Any(e => e.id == id);
        }
    }
}
