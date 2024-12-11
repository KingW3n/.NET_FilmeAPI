using System.ComponentModel.DataAnnotations;

namespace Filmes_API.Data.DTO
{
    public class ReadFilmeDto
    {
        public int Id { get; set; } 
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Duracao { get; set; }
        public  DateTime HoraConsulta { get; set; } = DateTime.Now;
    }
}
