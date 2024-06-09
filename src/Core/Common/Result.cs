namespace EfCoreDto.Core.Common;

public class Result
{
	protected Result(bool isSuccess, Error? error)
	{
		if (isSuccess && error != null)
		{
			throw new InvalidOperationException("A success result can not contain an error.");
		}

		if (!isSuccess && error == null)
		{
			throw new InvalidOperationException("A failure result must contain an error.");
		}

		this.IsSuccess = isSuccess;
		this.Error = error;
	}

	public bool IsSuccess { get; }

	public bool IsFailure => !this.IsSuccess;

	public Error? Error { get; }

	public static Result Success() => new(true, null);
	public static Result<TValue> Success<TValue>(TValue? value) where TValue : class => new(value, true, null);

	public static Result Fail(Error error) => new(false, error);
	public static Result<TValue> Fail<TValue>(Error error) where TValue : class => new(null, false, error);

	public static Result Fail(Exception exception) => Fail(exception.ToError());
	public static Result<TValue> Fail<TValue>(Exception exception) where TValue : class => Fail<TValue>(exception.ToError());
}

public class Result<TValue> : Result
	where TValue : class
{
	private readonly TValue? _value;

	protected internal Result(TValue? value, bool isSuccess, Error? error)
		: base(isSuccess, error) => _value = value;

	public TValue? Value()
	{
		if (this.IsFailure)
		{
			throw new InvalidOperationException("Can not access the value of a failure result.");
		}

		return _value;
	}
}
