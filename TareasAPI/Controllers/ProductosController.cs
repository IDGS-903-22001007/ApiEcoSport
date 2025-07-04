using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TareasAPI.Models;
using Microsoft.AspNetCore.Http;
using System;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly TiendaContext _context;

    public ProductosController(TiendaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        try
        {
            return await _context.Productos.ToListAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductos), new { id = producto.IdProducto }, producto);
    }

    [HttpPost("con-imagen")]
    public async Task<ActionResult<Producto>> PostProductoConImagen([FromForm] ProductoConImagenDto dto)
    {
        try
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                IdCategoria = dto.IdCategoria
            };

            if (dto.Imagen != null && dto.Imagen.Length > 0)
            {
                var rutaCarpeta = Path.Combine("wwwroot", "imagenes");
                Directory.CreateDirectory(rutaCarpeta);
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(dto.Imagen.FileName);
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await dto.Imagen.CopyToAsync(stream);
                }

                producto.ImagenUrl = $"/imagenes/{nombreArchivo}";
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductos), new { id = producto.IdProducto }, producto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}