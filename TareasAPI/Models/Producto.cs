using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TareasAPI.Models;

public partial class Producto
{
    [Key]
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public decimal Precio { get; set; }
    public string ImagenUrl { get; set; } = null!;

    public int IdCategoria { get; set; }

    [ForeignKey("IdCategoria")]
    public virtual Categoria Categoria { get; set; } = null!;
}