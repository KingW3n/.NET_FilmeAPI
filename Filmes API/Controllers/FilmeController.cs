using AutoMapper;
using Filmes_API.Data;
using Filmes_API.Data.DTO;
using Filmes_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Filmes_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context,IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
             Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges(); 
            return CreatedAtAction(nameof(RecuperarFilmesById), new {id = filme.Id}, filme);
        }
        /**
         * <summary>
         * Busca um filme no banco de dados através do ID
         * </summary>
         * */

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesById(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);   
            return Ok(filmeDto);
        }

        [HttpGet]
        public IEnumerable<ReadFilmeDto> RecuperarFilmesByIdAndPag([FromQuery] int skip = 0, [FromQuery] int take = 1000000)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
          
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            _mapper.Map( filmeDto, filme);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmePath(int id, JsonPatchDocument<UpdateFilmeDto> path)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            var filmeByUpdate = _mapper.Map<UpdateFilmeDto>(filme);

            path.ApplyTo(filmeByUpdate, ModelState);

            if (!TryValidateModel(filmeByUpdate)) return ValidationProblem(ModelState);

            _mapper.Map(filmeByUpdate, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

