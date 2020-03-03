using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasaDeShows.Data;
using CasaDeShows.Models;
using Microsoft.AspNetCore.Authorization;

namespace CasaDeShows.controllers
{
    [Authorize]
    [Authorize(Policy = "Administrador")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var email = User.Identity.Name;
            return View(await _context.casasDeShow.Where(cs => cs.User.UserName.Equals(email)).ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casasDeShow = await _context.casasDeShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (casasDeShow == null)
            {
                return NotFound();
            }

            return View(casasDeShow);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewBag.casaDeShow = _context.casasDeShow.ToList();
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Local")] CasasDeShow casasDeShow)
        {
            if (ModelState.IsValid)
            {
                var email = User.Identity.Name;
                var user = _context.Users.Where(use => use.Email.Equals(email)).FirstOrDefault();
                casasDeShow.User = user;
                _context.Add(casasDeShow);
                await _context.SaveChangesAsync();
                ViewBag.casaDeShow = _context.casasDeShow.First(p => p.Id == casasDeShow.Id);
                return RedirectToAction(nameof(Index));
            }
           
            return View(casasDeShow);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casasDeShow = await _context.casasDeShow.FindAsync(id);
            if (casasDeShow == null)
            {
                return NotFound();
            }
            ViewBag.casaDeShow = _context.casasDeShow.ToList();
            return View(casasDeShow);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Local")] CasasDeShow casasDeShow)
        {
            if (id != casasDeShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(casasDeShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CasasDeShowExists(casasDeShow.Id))
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
            return View(casasDeShow);
        }

       

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casasDeShow = await _context.casasDeShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (casasDeShow == null)
            {
                return NotFound();
            }

            return View(casasDeShow);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var casasDeShow = await _context.casasDeShow.FindAsync(id);
            _context.casasDeShow.Remove(casasDeShow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CasasDeShowExists(int id)
        {
            return _context.casasDeShow.Any(e => e.Id == id);
        }
    }
}
