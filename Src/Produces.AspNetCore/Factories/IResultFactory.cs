using Microsoft.AspNetCore.Http;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories;

public interface IResultFactory
{
    IResult Create(IProduce result, HttpContext context);
}