using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyncAPI.Models;

namespace SyncAPI.Services
{
    public interface IVisitasService
    {
        Task<IEnumerable<Visitas>> GetAllVisitasAsync();
        Task<Visitas> GetVisitaByIdAsync(Guid id);
        Task AddVisitaAsync(Visitas visita);
        Task UpdateVisitaAsync(Visitas visita);
        Task DeleteVisitaAsync(Guid id);
    }
}