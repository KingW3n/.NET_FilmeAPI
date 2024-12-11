using System.ComponentModel.DataAnnotations;

namespace Filmes_API.Data.DTO
{
    public class CreateFilmeDto
    {
        [Required(ErrorMessage = "O titulo do filme é obrigatorio")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O genero do filme é obrigatorio")]
        [StringLength(50)]
        public string Genero { get; set; }
        [Required]
        [Range(60, 600, ErrorMessage = "A duracao do filme deve ser entre 60 e 600 minutos")]
        public int Duracao { get; set; }
    }
}
