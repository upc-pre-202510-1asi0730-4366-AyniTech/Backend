using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Lot.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;

namespace Lot.Reports.Infrastructure.Persistence.EFC.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Report report)
        {
            await _context.Set<Report>().AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Report>> ListAsync()
        {
            return await _context.Set<Report>().ToListAsync();
        }
        
        public async Task<IEnumerable<Report>> ListByCategoryAsync()
        {
            return await _context.Set<Report>()
                .OrderBy(r => r.Field.Name)  
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> ListByProductAsync()
        {
            return await _context.Set<Report>()
                .OrderBy(r => r.Field.Name) 
                .ToListAsync();
        }
        
        public async Task<IEnumerable<StockAverageReport>> ListStockAverageReportsAsync()
        {
            return await _context.Set<StockAverageReport>().ToListAsync();
        }


    }
}

/*using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Lot.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;

namespace Lot.Reports.Infrastructure.Persistence.EFC.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Report report)
    {
        await _context.Set<Report>().AddAsync(report);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Report>> ListAsync()
    {
        return await _context.Set<Report>().ToListAsync();
    }

    public async Task<IEnumerable<Report>> GetReportsByCategoryAsync()
    {
        return await _context.Set<Report>()
            .OrderBy(r => r.Field.Name) // o r.Category si tienes esa propiedad
            .ToListAsync();
    }

    public async Task<IEnumerable<Report>> GetReportsByProductAsync()
    {
        return await _context.Set<Report>()
            .OrderBy(r => r.Field.Name) // o r.Product si existe
            .ToListAsync();
    }

    public async Task<IEnumerable<StockAverage>> GetStockAveragesAsync()
    {
        return await _context.Set<StockAverage>().ToListAsync();
    }
}
*/