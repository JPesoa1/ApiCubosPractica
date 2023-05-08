using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCubosPractica.Models
{
    [Table("COMPRACUBOS")]
    public class Compra
    {
        [Key]
        [Column("id_pedido")]
        public int Idpedido { get; set; }

        [Column("id_cubo")]
        public int IdCubo{ get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("fechapedido")]
        public DateTime Fecha { get; set; }
    }
}
