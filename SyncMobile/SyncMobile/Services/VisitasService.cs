using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SyncMobile.Context;
using SyncMobile.Models;

public class VisitasService
{
    private readonly SyncContext _dbContext;

    public VisitasService()
    {
        _dbContext = new SyncContext();
    }

    public async Task<List<Visita>> GetVisitasAsync()
    {
        return await _dbContext.Visitas.ToListAsync();
    }
}
