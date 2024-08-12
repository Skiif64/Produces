using System.Runtime.InteropServices.JavaScript;
using Produces.Core;
using Produces.Core.Errors;
using Produces.Example.Endpoints;
using Produces.Example.Repositories;

namespace Produces.Example.Services;

public static class UsersService
{
    public static Produce Register(RegisterModel model)
    {
        if (UserRepository.GetByEmail(model.Email) is not null)
        {
            return Produce.Failure(Error.Conflict("https://example.com/email-exists",
                "Email already exists",
                $"User with email {model.Email} already exists",
                new
                {
                    Email = model.Email
                }));
        }

        var user = new User(model.Email, model.Password);
        UserRepository.Create(user);
        return Produce.Success();
    }

    public static Produce<User> Login(LoginModel model)
    {
        var user = UserRepository.GetByEmail(model.Email);
        if (user is null || user.Password != model.Password)
        {
            return Produce
                .Failure<User>(Error.Problem("about:blank",
                    "Login failed",
                    "Email or password invalid",
                    null));
        }

        return Produce.Success(user);
    }
}