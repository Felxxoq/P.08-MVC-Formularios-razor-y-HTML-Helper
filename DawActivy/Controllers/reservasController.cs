﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DawActivy.Models;

namespace DawActivy.Controllers
{
    public class reservasController : Controller
    {
        private readonly equipoDbContext _context;

        public reservasController(equipoDbContext context)
        {
            _context = context;
        }

        // GET: reservas
        public async Task<IActionResult> Index()
        {
              return _context.reservas != null ? 
                          View(await _context.reservas.ToListAsync()) :
                          Problem("Entity set 'equipoDbContext.reservas'  is null.");
        }

        // GET: reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.reservas
                .FirstOrDefaultAsync(m => m.reserva_id == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // GET: reservas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("reserva_id,equipo_id,usuario_id,fecha_salida,hora_salida,tiempo_reserva,estado_reserva,fecha_retorno,hora_retorno,estado")] reservas reservas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservas);
        }

        // GET: reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }
            return View(reservas);
        }

        // POST: reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("reserva_id,equipo_id,usuario_id,fecha_salida,hora_salida,tiempo_reserva,estado_reserva,fecha_retorno,hora_retorno,estado")] reservas reservas)
        {
            if (id != reservas.reserva_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!reservasExists(reservas.reserva_id))
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
            return View(reservas);
        }

        // GET: reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.reservas
                .FirstOrDefaultAsync(m => m.reserva_id == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // POST: reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.reservas == null)
            {
                return Problem("Entity set 'equipoDbContext.reservas'  is null.");
            }
            var reservas = await _context.reservas.FindAsync(id);
            if (reservas != null)
            {
                _context.reservas.Remove(reservas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool reservasExists(int id)
        {
          return (_context.reservas?.Any(e => e.reserva_id == id)).GetValueOrDefault();
        }
    }
}
