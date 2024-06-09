namespace EfCoreDto.WebApi.Extensions;

public static class ResultExtension
{
	public static ProblemDetails ToProblem(this Result result)
	{
		if (result.IsSuccess)
		{
			throw new InvalidOperationException("Can't convert success result to problem.");
		}

		return result.Error!.ToProblem();
	}
}
