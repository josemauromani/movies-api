using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using movies_api.Data;
using movies_api.Data.DTOs;
using movies_api.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public MovieController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme a base de dados
    /// </summary>
    /// <param name="movieDTO">Objeto com os campos necessarios para criacao de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a insercao seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddMovie([FromBody] CreateMovieDTO movieDTO)
    {
        Movie movie = _mapper.Map<Movie>(movieDTO);
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetMovie), new { id = movie.id }, movie);
    }

    [HttpGet]
    public IEnumerable<ReadMovieDTO> ListMovies([FromQuery] int skip = 0, [FromQuery] int take = 5)
    {
        return _mapper.Map<List<ReadMovieDTO>>(_context.Movies.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult GetMovie(Guid id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.id == id);
        if (movie == null) return NotFound();
        var movieDTO = _mapper.Map<ReadMovieDTO>(movie);
        return Ok(movieDTO);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(string id, [FromBody] UpdateMovieDTO movieDTO)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.id == new Guid(id));
        if (movie == null) return NotFound();
        _mapper.Map(movieDTO, movie);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult PatchMovie(string id, JsonPatchDocument<UpdateMovieDTO> patch)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.id == new Guid(id));
        if (movie == null) return NotFound();

        var movieToUpdate = _mapper.Map<UpdateMovieDTO>(movie);
        patch.ApplyTo(movieToUpdate, ModelState);
        if (!TryValidateModel(movieToUpdate)) return ValidationProblem(ModelState);

        _mapper.Map(movieToUpdate, movie);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(string id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.id == new Guid(id));
        if (movie == null) return NotFound();

        _context.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }

}