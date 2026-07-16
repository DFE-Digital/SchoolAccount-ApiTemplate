using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Endpoints.Organisation.GetByLaestab;

public sealed record GetByLaestabRequest
{
    [FromRoute]
    [RegularExpression(@"^\d{7}$", ErrorMessage = "LAESTAB identifiers are 7 character numeric only values in the format 1234567")]
    public int Laestab { get; init; }
};
