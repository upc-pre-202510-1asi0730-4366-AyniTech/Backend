using Lot.Reports.Domain.Model.Commands;
using Lot.Reports.Domain.Services;
using Lot.Reports.Interfaces.REST.Resources;
using Lot.Reports.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Lot.Reports.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportCommandService _reportCommandService;
    private readonly IReportQueryService _reportQueryService;
    
    private readonly IStockAverageQueryService _stockAverageQueryService;
    private readonly ICategoryReportQueryService _categoryReportQueryService;
    
    public ReportController(
        IReportCommandService reportCommandService,
        IReportQueryService reportQueryService,
        IStockAverageQueryService stockAverageQueryService,
        ICategoryReportQueryService categoryReportQueryService) 
    {
        _reportCommandService = reportCommandService;
        _reportQueryService = reportQueryService;
        _stockAverageQueryService = stockAverageQueryService;
        _categoryReportQueryService = categoryReportQueryService;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReportResource resource)
    {
        var command = new CreateReportCommand(resource.FieldName, resource.FilterStartDate, resource.FilterEndDate);
        var result = await _reportCommandService.CreateAsync(command);
        return Created("", new { message = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _reportQueryService.GetAllAsync();
        var resources = reports.Select(ReportResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpGet("by-field")]
    public async Task<IActionResult> GetByField([FromQuery] string fieldName)
    {
        var reports = await _reportQueryService.GetByFieldNameAsync(fieldName);
        var resources = reports.Select(ReportResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var reports = await _reportQueryService.GetByDateRangeAsync(startDate, endDate);
        var resources = reports.Select(ReportResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }
    
    [HttpGet("by-category")]
    public async Task<IEnumerable<ReportResource>> GetReportsByCategoryAsync()
    {
        var reports = await _reportQueryService.GetReportsByCategoryAsync();
        return reports.Select(ReportResourceFromEntityAssembler.ToResource);
    }

    [HttpGet("by-product")]
    public async Task<IEnumerable<ReportResource>> GetReportsByProductAsync()
    {
        var reports = await _reportQueryService.GetReportsByProductAsync();
        return reports.Select(ReportResourceFromEntityAssembler.ToResource);
    }
    
    [HttpGet("stock-average")]
    public async Task<IActionResult> GetStockAverageReports()
    {
        var result = await _stockAverageQueryService.ListStockAverageReportsAsync();
        var resources = result.Select(StockAverageResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

   
    [HttpGet("by-category-details")]
    public async Task<IActionResult> GetCategoryReports()
    {
        var result = await _categoryReportQueryService.ListAsync();
        var resources = result.Select(CategoryReportResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }


}