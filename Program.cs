﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ReproduceMiddlewareStreamBug
{
	class Program
	{
		static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateHostBuilder(string[] args)
			=> WebHost.CreateDefaultBuilder()
				.UseStartup<Startup>()
				.ConfigureKestrel(
					options =>
					{
						options.AddServerHeader = false;
					});
	}
}
