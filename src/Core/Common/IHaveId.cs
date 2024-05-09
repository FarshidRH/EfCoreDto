namespace EfCoreDto.Core.Common;

public interface IHaveId<T>
{
    T Id { get; }
}
