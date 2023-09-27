using CleanArchitecture.Application.Abstractions.Models;

namespace MediatR;

public interface IPaginatedRequest<T> : IRequest<PaginatedResult<T>>
{
    int PageIndex { get; init; }
    int PageSize { get; init; }
}

public interface IPaginatedRequestHandler<in TRequest, TResponse> : IRequestHandler<TRequest, PaginatedResult<TResponse>>
    where TRequest : IPaginatedRequest<TResponse>
{
}