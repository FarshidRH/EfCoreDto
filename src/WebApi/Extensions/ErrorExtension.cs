namespace EfCoreDto.WebApi.Extensions;

public static class ErrorExtension
{
	public static ProblemDetails ToProblem(this Error error) => new()
	{
		Detail = error.Detail,
		Status = error.ToStatusCode(),
	};

	public static int ToStatusCode(this Error error) => error.Type switch
	{
		ErrorType.Invalid => Status400BadRequest,
		ErrorType.NotFound => Status404NotFound,
		ErrorType.Conflict => Status409Conflict,
		_ => Status500InternalServerError,
	};
}
