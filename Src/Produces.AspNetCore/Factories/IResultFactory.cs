using Microsoft.AspNetCore.Http;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories;

public interface IResultFactory
{
    IResult Create<TProduce>(TProduce result, HttpContext context)
        where TProduce : IProduce;
}