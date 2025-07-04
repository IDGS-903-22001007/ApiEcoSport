using Microsoft.AspNetCore.Http;

namespace TareasAPI.Models;

public class ProductoConImagenDto
{
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public decimal Precio { get; set; }
    public int IdCategoria { get; set; }
    public IFormFile? Imagen { get; set; }
}