namespace api.Options;

public static class Constants
{
    public const string AppTitle = "Animal-Crossing-NH-Collector.API";
    public const string AppVersion = "v1";
    public const string SwaggerEndpoint = "/swagger/v1/swagger.json";

    public static class RequestCodes
    {
        // Register-related error codes
        public const string FailureCreateAccount = "FailureCreateAccount";
        public const string FailurePasswordDoesNotMatch = "FailurePasswordDoesNotMatch";
        public const string FailureEmailExists = "FailureEmailExists";

        // Login-related error codes
        public const string UserNotFound = "UserNotFound";
        public const string CookieNotSet = "CookieNotSet";
        public const string LockedOut = "LockedOut";
        public const string NotAllowed = "NotAllowed";
        public const string PasswordIncorrect = "PasswordIncorrect";

        // Logout-related error codes
        public const string FailureLogout = "FailureLogout";

        // Password management error codes
        public const string FailurePasswordResetRequest = "FailurePasswordResetRequest";
        public const string FailurePasswordReset = "FailurePasswordReset";
        public const string FailureInvalidToken = "FailureInvalidToken";

        // Item-related error codes
        public const string ItemNotFound = "ItemNotFound";
        public const string UserItemNotFound = "UserItemNotFound";
        public const string FailureAddUserItem = "FailureAddUserItem";
        public const string FailureRemoveUserItem = "FailureRemoveUserItem";
        public const string FailureGetUserItems = "FailureGetUserItems";
    }

    internal static class RequestMessages
    {
        // Register-related messages
        public static readonly string SuccessCreateAccount = "Cuenta creada correctamente.";
        public static readonly string FailureCreateAccount = "Error al crear la cuenta.";
        public static readonly string FailurePasswordDoesNotMatch = "Las contraseñas no coinciden.";
        public static readonly string FailureEmailExists = "El correo electrónico ya está registrado.";

        // Login-related messages
        public static readonly string SuccessLogin = "Inicio de sesión correcto.";
        public static readonly string FailureLogin = "Error al iniciar sesión.";
        public static readonly string FailureUserNotFound = "Usuario no encontrado.";
        public static readonly string FailureCookieNotSet = "Error al establecer la cookie.";
        public static readonly string FailureLockout = "Usuario bloqueado.";
        public static readonly string FailurePasswordIncorrect = "Contraseña incorrecta.";

        // Logout-related messages
        public static readonly string SuccessLogout = "Cierre de sesión correcto.";
        public static readonly string FailureLogout = "Error al cerrar sesión.";

        // Password management messages
        public static readonly string SuccessPasswordResetRequest = "Solicitud de restablecimiento de contraseña enviada.";
        public static readonly string FailurePasswordResetRequest = "Error al solicitar el restablecimiento de contraseña.";
        public static readonly string SuccessPasswordReset = "Contraseña restablecida correctamente.";
        public static readonly string FailurePasswordReset = "Error al restablecer la contraseña.";
        public static readonly string FailureInvalidToken = "Token de restablecimiento de contraseña inválido.";

        // Item-related messages
        public static readonly string ItemNotFound = "Ítem no encontrado.";
        public static readonly string UserItemNotFound = "Ítem del usuario no encontrado.";
        public static readonly string SuccessAddUserItem = "Ítem agregado al usuario correctamente.";
        public static readonly string FailureAddUserItem = "Error al intentar agregar el ítem al usuario.";
        public static readonly string SuccessGetUserItems = "Ítems del usuario obtenidos correctamente.";
        public static readonly string SuccessRemoveUserItem = "Ítem eliminado correctamente.";
        public static readonly string FailureRemoveUserItem = "Error al intentar eliminar el ítem del usuario.";
        public static readonly string FailureGetUserItems = "Error al intentar obtener los ítems del usuario.";
        public static readonly string NoItemsFound = "No se encontraron ítems para el usuario.";
    }
}
