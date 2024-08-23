using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecFlowConjunta.Models
{
    [Table("Products")] // Especifica el nombre de la tabla en la base de datos
    public class Product // Renombrado para reflejar mejor lo que representa
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string Category { get; set; } // Category no es [Required] en la definición de la tabla SQL

        [Required]
        [Column(TypeName = "decimal(10, 2)")] // Especifica el tipo de dato exacto en SQL Server
        public decimal Price { get; set; } // Cambiado de float a decimal para precisión monetaria

        [Required]
        public int StockQuantity { get; set; }
    }
}
