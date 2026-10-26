using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(254)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo da categoria é obrigatório.")]
        [Column("tipo")]
        public TipoCategoria Tipo { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public ICollection<Transacao> Transacoes { get; set; }
    }
}
