using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Produces.AspNetCore.Extensions;
using Produces.Core;
using Produces.Core.Errors;
using Produces.Example.Repositories;
using Produces.Example.Services;

namespace Produces.Example.Endpoints;

public sealed class AuthEndpoints : IEndpoint
{
    public void ConfigureEndpoint(WebApplication app)
    {
        app.MapPost("~/auth/register", Register);
        app.MapPost("~/auth/login", Login);
    }

    private async Task<IResult> Register(HttpContext context,
        [FromServices] IValidator<RegisterModel> validator,
        [FromBody] RegisterModel model)
    {
        if (model.Email == "empty")
            return Produce.Failure(Error.NotFound()).ToHttpResult(context);
        
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return ToError(validationResult).ToHttpResult(context);
        }

        var result = UsersService.Register(model);
        return result.ToHttpResult(context);
    }

    private async Task<IResult> Login(HttpContext context,
        [FromServices] IValidator<LoginModel> validator,
        [FromBody] LoginModel model)
    {
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return ToError(validationResult).ToHttpResult(context);
        }
        var result = UsersService.Login(model);
        return result.ToHttpResult(context);
    }

private static Produce ToError(ValidationResult result)
    {
        var failures = result.Errors.Select(x 
            => new Core.Errors.ValidationFailure(x.PropertyName, x.ErrorMessage, x.AttemptedValue));
        return Error.Validation("https://example.com/validation/register", failures);
    }
}

public class RegisterModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}

public class LoginModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public sealed class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email should be valid address");
        RuleFor(x => x.Password)
            .Must((model, password) => password.Equals(model.ConfirmPassword))
            .WithMessage("Password should be same as confirm password");
    }
}

public sealed class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email should be valid address");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty");
    }
}