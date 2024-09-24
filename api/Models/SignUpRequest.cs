using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class SignUpRequest
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Repetir la contraseña es obligatorio.")]
    public string RepeatedPassword { get; set; }
}
