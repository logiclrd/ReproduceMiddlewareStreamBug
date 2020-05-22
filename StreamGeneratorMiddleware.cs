using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace ReproduceMiddlewareStreamBug
{
	class StreamGeneratorMiddleware
	{
		public StreamGeneratorMiddleware(RequestDelegate next)
		{
			var streamData = new List<byte[]>();

			using (var reader = new StreamReader(typeof(StreamGeneratorMiddleware).Assembly.GetManifestResourceStream("ReproduceMiddlewareStreamBug.StreamData.txt")))
			{
				while (true)
				{
					string line = reader.ReadLine();

					if (line == null)
						break;

					streamData.Add(Encoding.UTF8.GetBytes(line + "\r\n"));
				}
			}

			_streamData = streamData.ToArray();
		}

		byte[][] _streamData;

		public async Task InvokeAsync(HttpContext context)
		{
			context.Response.ContentType = "text/plain";

			int streamDataIndex = 0;

			while (true)
			{
				Console.WriteLine("Still streaming {0}", streamDataIndex);

				await context.Response.Body.WriteAsync(_streamData[streamDataIndex]);
				await context.Response.Body.FlushAsync();

				streamDataIndex++;

				if (streamDataIndex >= _streamData.Length)
					streamDataIndex = 0;

				Thread.Sleep(250);
			}
		}
	}
}
