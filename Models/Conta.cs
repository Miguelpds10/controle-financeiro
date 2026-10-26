using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    public class Conta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(254)]
        public string? Nome { get; set; }

        [Required]
        [Range(0.01, 999999.99, ErrorMessage = "O saldo deve ser maior que zero.")]
        [Column(TypeName = "numeric(12,2)")]
        public decimal Saldo { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
