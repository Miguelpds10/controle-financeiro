using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(254)]
        public string? Nome { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string? SenhaHash { get; set; }

    }
}
