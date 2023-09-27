namespace CleanArchitecture.Application.Abstractions.Models;

public interface IResult<T>
{
    T Data { get; set; }
    string[] Errors { get; set; }
    bool IsSuccess { get; set; }

    static IResult<T> Success(T data)
    {
       return new ApiResult<T>
       {
           Data = data,
           IsSuccess = true
       };
    }

    static IResult<T> Error(string[] errors)
    {
       return new ApiResult<T>
       {
           Errors = errors,
           IsSuccess = false
       };
    }
}

public record ApiResult<T> : IResult<T>
{
    public T Data { get; set; } = default!;
    public string[] Errors { get; set; } = Array.Empty<string>();
    public bool IsSuccess { get; set; }
}

public record PaginatedResult<T> : IResult<T[]>
{
    public T[] Data { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public string[] Errors { get; set; } = Array.Empty<string>();
    public bool IsSuccess { get; set; }
    
    public static PaginatedResult<T> Success(T[] data, int totalCount, int pageIndex, int pageSize)
    {
        return new PaginatedResult<T>
        {
            Data = data,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize,
            HasPreviousPage = pageIndex > 1,
            HasNextPage = (pageIndex * pageSize) < totalCount,
            IsSuccess = true
        };
    }
}