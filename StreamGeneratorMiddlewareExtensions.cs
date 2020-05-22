using Microsoft.AspNetCore.Builder;

namespace ReproduceMiddlewareStreamBug
{
	static class StreamGeneratorMiddlewareExtensions
	{
		public static IApplicationBuilder UseStreamGenerator(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<StreamGeneratorMiddleware>();
		}
	}
}
