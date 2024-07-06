using System.Reflection;

namespace EfCoreDto.WebApi;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
