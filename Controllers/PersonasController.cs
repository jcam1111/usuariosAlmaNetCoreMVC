using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UsuariosAlmaNetCoreMVC.Data;
using UsuariosAlmaNetCoreMVC.Models;

namespace UsuariosAlmaNetCoreMVC.Controllers
{
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
              return _context.Personas != null ? 
                          View(await _context.Personas.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Personas'  is null.");
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            ViewData["TipoIdentificacionId"] = new SelectList( _context.TiposIdentificacion.ToList(), "Id", "Nombre");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,NumeroIdentificacion,Email,TipoIdentificacion,FechaCreacion")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoIdentificacionId"] = new SelectList(await _context.TiposIdentificacion.ToListAsync(), "Id", "Nombre", persona.TipoIdentificacionId);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            ViewData["TipoIdentificacionId"] = new SelectList(await _context.TiposIdentificacion.ToListAsync(), "Id", "Nombre", persona.TipoIdentificacionId);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,NumeroIdentificacion,Email,TipoIdentificacion,FechaCreacion")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
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
            ViewData["TipoIdentificacionId"] = new SelectList(await _context.TiposIdentificacion.ToListAsync(), "Id", "Nombre", persona.TipoIdentificacionId);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Personas'  is null.");
            }
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return (_context.Personas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
