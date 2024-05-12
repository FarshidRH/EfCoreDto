namespace EfCoreDto.Core.Common;

public interface IHaveId<out T>
{
	T Id { get; }
}
