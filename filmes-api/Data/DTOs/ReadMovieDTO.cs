using System;
using System.ComponentModel.DataAnnotations;

namespace movies_api.Data.DTOs;

public class ReadMovieDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public DateTime SearchHour { get; set; } = DateTime.Now;
}

