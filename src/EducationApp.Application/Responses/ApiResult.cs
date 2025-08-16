namespace EducationApp.Application.Responses;

public record ApiResult<T>
    (string Message = null!, bool Success = false , T Data = default!);