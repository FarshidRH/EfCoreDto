using Asp.Versioning;

namespace EfCoreDto.WebApi.Endpoints;

public static class Versions
{
	public static readonly ApiVersion _1_0 = new(1);
	public static readonly ApiVersion _2_0 = new(2);
}
