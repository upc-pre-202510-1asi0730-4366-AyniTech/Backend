using System.Net.Mime;
using Lot.Inventaries.Domain.Model.Queries;
using Lot.Inventaries.Domain.Model.ValueOjbects;
using Lot.Inventaries.Domain.Services;
using Lot.Inventaries.Interfaces.REST.Resources;
using Lot.Inventaries.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lot.Inventaries.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Puntos de acceso disponibles para Inventarios (Lotes).")]
public class InventariesController(
    IInventaryCommandService inventaryCommandService,
    IInvetaryQueryService inventaryQueryService)
    : ControllerBase
{
    [HttpGet("{inventaryId:int}")]
    [SwaggerOperation("Obtener Inventario por Id", "Obtiene un inventario por su identificador único.", OperationId = "GetInventaryById")]
    [SwaggerResponse(200, "El inventario fue encontrado y retornado.", typeof(InventarieResource))]
    [SwaggerResponse(404, "El inventario no fue encontrado.")]
    public async Task<IActionResult> GetInventaryById(int inventaryId)
    {
      
        var inventaries = await inventaryQueryService.Handle(new GetAllLotByEntryDate(DateTime.MinValue));
        var entity = inventaries.FirstOrDefault(i => i.Id == inventaryId);
        if (entity is null) return NotFound();
        var resource = InventarieResourceFromEntityAssembler.ToResourceFromEntity(entity);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation("Crear Inventario", "Crea un nuevo inventario (lote).", OperationId = "CreateInventary")]
    [SwaggerResponse(201, "El inventario fue creado.", typeof(InventarieResource))]
    [SwaggerResponse(400, "El inventario no fue creado.")]
    public async Task<IActionResult> CreateInventary(CreateInventarieResource resource)
    {
        var createInventaryCommand = CreateInventarieCommandFromResourceAssembler.ToCommandFromResource(resource);
        var inventary = await inventaryCommandService.Handle(createInventaryCommand);
        if (inventary is null) return BadRequest();
        var inventaryResource = InventarieResourceFromEntityAssembler.ToResourceFromEntity(inventary);
        return CreatedAtAction(nameof(GetInventaryById), new { inventaryId = inventary.Id }, inventaryResource);
    }

    [HttpGet]
    [SwaggerOperation("Obtener Todos los Inventarios", "Obtiene todos los inventarios (lotes).", OperationId = "GetAllInventaries")]
    [SwaggerResponse(200, "Los inventarios fueron encontrados y retornados.", typeof(IEnumerable<InventarieResource>))]
    public async Task<IActionResult> GetAllInventaries()
    {
        var inventaries = await inventaryQueryService.Handle(new GetAllLotByEntryDate(DateTime.MinValue));
        var inventaryResources = inventaries.Select(InventarieResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(inventaryResources);
    }

    [HttpGet("by-entry-date")]
    [SwaggerOperation("Obtener Inventarios por Fecha de Ingreso", "Obtiene los inventarios por fecha de ingreso.", OperationId = "GetInventariesByEntryDate")]
    [SwaggerResponse(200, "Inventarios encontrados.", typeof(IEnumerable<InventarieResource>))]
    public async Task<IActionResult> GetInventariesByEntryDate([FromQuery] DateTime entryDate)
    {
        var inventaries = await inventaryQueryService.Handle(new GetAllLotByEntryDate(entryDate));
        var resources = inventaries.Select(InventarieResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("by-price")]
    [SwaggerOperation("Obtener Inventarios por Precio", "Obtiene los inventarios por precio.", OperationId = "GetInventariesByPrice")]
    [SwaggerResponse(200, "Inventarios encontrados.", typeof(IEnumerable<InventarieResource>))]
    public async Task<IActionResult> GetInventariesByPrice([FromQuery] decimal price)
    {
        var inventaries = await inventaryQueryService.Handle(new GetAllLotByPrice(price));
        var resources = inventaries.Select(InventarieResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("by-product")]
    [SwaggerOperation("Obtener Inventarios por Producto", "Obtiene los inventarios por producto.", OperationId = "GetInventariesByProduct")]
    [SwaggerResponse(200, "Inventarios encontrados.", typeof(IEnumerable<InventarieResource>))]
    public async Task<IActionResult> GetInventariesByProduct([FromQuery] string product)
    {
        var inventaries = await inventaryQueryService.Handle(new GetAllLotByProduct(new ProductName(product)));
        var resources = inventaries.Select(InventarieResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("by-supplier")]
    [SwaggerOperation("Obtener Inventarios por Proveedor", "Obtiene los inventarios por proveedor.", OperationId = "GetInventariesBySupplier")]
    [SwaggerResponse(200, "Inventarios encontrados.", typeof(IEnumerable<InventarieResource>))]
    public async Task<IActionResult> GetInventariesBySupplier([FromQuery] string supplier)
    {
        var inventaries = await inventaryQueryService.Handle(new GetAllLotBySupplier(new Domain.Model.ValueOjbects.Supplier(supplier)));
        var resources = inventaries.Select(InventarieResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("by-quantity")]
    [SwaggerOperation("Obtener Inventarios por Cantidad", "Obtiene los inventarios por cantidad.", OperationId = "GetInventariesByQuantity")]
    [SwaggerResponse(200, "Inventarios encontrados.", typeof(IEnumerable<InventarieResource>))]
    public async Task<IActionResult> GetInventariesByQuantity([FromQuery] int quantity)
    {
        var inventaries = await inventaryQueryService.Handle(new GetAlLotByQuantity(quantity));
        var resources = inventaries.Select(InventarieResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}