using Microsoft.AspNetCore.Mvc;

using static api.Options.Constants;

namespace api.Models;

public class Result<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string ErrorCode { get; set; }
    public IList<string> Errors { get; set; } = new List<string>();

    public static Result<T> Success(T? data = default, string? message = null)
    {
        return new Result<T> { Data = data, Message = message };
    }

    public static Result<T> Failure(string errorCode, string message, IList<string> errors = null)
    {
        return new Result<T> { ErrorCode = errorCode, Message = message, Errors = errors ?? new List<string>() };
    }

    public IActionResult ToActionResult()
    {
        if (Errors.Any())
        {
            return new BadRequestObjectResult(new { Errors });
        }

        if (string.IsNullOrEmpty(ErrorCode))
        {
            return new OkObjectResult(new { Data, Message });
        }

        return ErrorCode switch
        {
            RequestCodes.UserNotFound => new NotFoundObjectResult(new { Message = RequestMessages.FailureUserNotFound }),
            RequestCodes.CookieNotSet => new StatusCodeResult(StatusCodes.Status401Unauthorized),
            RequestCodes.LockedOut => new StatusCodeResult(StatusCodes.Status401Unauthorized),
            RequestCodes.NotAllowed => new StatusCodeResult(StatusCodes.Status401Unauthorized),
            RequestCodes.PasswordIncorrect => new StatusCodeResult(StatusCodes.Status401Unauthorized),

            RequestCodes.FailureCreateAccount => new BadRequestObjectResult(new { Message = RequestMessages.FailureCreateAccount }),
            RequestCodes.FailurePasswordDoesNotMatch => new BadRequestObjectResult(new { Message = RequestMessages.FailurePasswordDoesNotMatch }),
            RequestCodes.FailureEmailExists => new BadRequestObjectResult(new { Message = RequestMessages.FailureEmailExists }),

            RequestCodes.FailurePasswordResetRequest => new BadRequestObjectResult(new { Message = RequestMessages.FailurePasswordResetRequest }),
            RequestCodes.FailurePasswordReset => new BadRequestObjectResult(new { Message = RequestMessages.FailurePasswordReset }),
            RequestCodes.FailureInvalidToken => new BadRequestObjectResult(new { Message = RequestMessages.FailureInvalidToken }),
          
            _ => new BadRequestObjectResult(new { Message = Message })
        };
    }
}
