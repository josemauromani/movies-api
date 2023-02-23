using System;
using System.ComponentModel.DataAnnotations;

namespace movies_api.Data.DTOs;

public class UpdateMovieDTO
{
    [Required(ErrorMessage = "O campo titulo e obrigatorio")]
    public string Title { get; set; }

    [Required(ErrorMessage = "O campo genero e obrigatorio")]
    [StringLength(50, ErrorMessage = "O tamanho nao pode exceder 10 caracteres")]
    public string Genre { get; set; }

    [Required]
    [Range(15, 600, ErrorMessage = "A duracao deve ter entre 15 min e 600 min")]
    public int Duration { get; set; }
}

