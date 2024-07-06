using System.Reflection;

namespace EfCoreDto.Core;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
