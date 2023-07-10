using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Behaviours;

public delegate Result<TResponse> ValidateDelegate<in TRequest, TResponse>(TRequest request);
