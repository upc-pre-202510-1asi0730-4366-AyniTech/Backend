using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Lot.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;

namespace Lot.Reports.Infrastructure.Persistence.EFC.Repositories;

    
public class CategoryReportRepository : ICategoryReportRepository
{
    private readonly AppDbContext _context;

    public CategoryReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryReport>> ListAsync()
    {
        return await _context.Set<CategoryReport>().ToListAsync();
    }
}