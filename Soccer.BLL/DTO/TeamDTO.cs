using System.ComponentModel.DataAnnotations;

namespace Soccer.BLL.DTO;

// Data Transfer Object — спеціальна модель для передачі даних.
// Клас TeamDTO повинен містити лише ті дані, які потрібно передати 
// на рівень представлення або, навпаки, отримати з цього рівня.
public class TeamDTO
{
    public int Id { get; set; }

    [Display(Name = "Назва клубу")]
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    public required string Name { get; set; }

    [Display(Name = "Тренер клубу")]
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    public required string Coach { get; set; }
}