using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SyncAPI.Models;
using Microsoft.EntityFrameworkCore;
using SyncAPI.Data;

namespace SyncAPI.Services
{
    public class VisitasService : IVisitasService
    {
        private readonly SyncAPIDbContext _context;

        public VisitasService(SyncAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Visitas>> GetAllVisitasAsync()
        {
            return await _context.Visitas.ToListAsync();
        }

        public async Task<Visitas> GetVisitaByIdAsync(Guid id)
        {
            return await _context.Visitas.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddVisitaAsync(Visitas visita)
        {
            _context.Visitas.Add(visita);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVisitaAsync(Visitas visita)
        {
            _context.Entry(visita).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitaAsync(Guid id)
        {
            var visita = await _context.Visitas.FindAsync(id);
            if (visita != null)
            {
                _context.Visitas.Remove(visita);
                await _context.SaveChangesAsync();
            }
        }
    }
}
