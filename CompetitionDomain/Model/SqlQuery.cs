using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CompetitionDomain.Model;

public partial class SqlQuery
{
    public int Id { get; set; }

    [Display(Name = "Опис")]
    [Required(ErrorMessage = "Поле 'Опис' є обов'язковим.")]
    [StringLength(255, ErrorMessage = "Поле 'Опис' не може перевищувати 255 символів.")]
    public string Name { get; set; } = null!;

    [Display(Name = "SQL")]
    [Required(ErrorMessage = "Поле 'SQL' є обов'язковим.")]
    [StringLength(4000, ErrorMessage = "Поле 'SQL' не може перевищувати 4000 символів.")]
    public string QueryText { get; set; } = null!;
}
