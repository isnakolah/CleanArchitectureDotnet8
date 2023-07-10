using FluentValidation.Results;
using OneOf.Types;
using OneOf;

namespace CleanArchitecture.Application.Common.Models;

[GenerateOneOf]
public partial class Result<T> : OneOfBase<T, ValidationFailure[], NotFound>
{
}

[GenerateOneOf]
public partial class ResultErrors : OneOfBase<ValidationFailure[], NotFound>
{
}