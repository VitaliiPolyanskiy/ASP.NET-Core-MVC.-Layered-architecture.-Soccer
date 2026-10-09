using System.ComponentModel.DataAnnotations;

namespace Soccer.BLL.DTO;

// Data Transfer Object — спеціальна модель для передачі даних.
// Клас PlayerDTO повинен містити лише ті дані, які потрібно передати 
// на рівень представлення або, навпаки, отримати з цього рівня.
public class PlayerDTO
{
    public int Id { get; set; }

    [Display(Name = "Ім'я гравця")]
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    public required string Name { get; set; }

    [Display(Name = "Рік народження гравця")]
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    public int BirthYear { get; set; }

    [Display(Name = "Позиція на полі")]
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    public required string Position { get; set; }

    public int? TeamId { get; set; }

    [Display(Name = "Назва клубу")]
    public string? Team { get; set; }
}