using Microsoft.AspNetCore.Builder;

namespace ReproduceMiddlewareStreamBug
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.UseStreamGenerator();
			app.UseDeveloperExceptionPage();
		}
	}
}
