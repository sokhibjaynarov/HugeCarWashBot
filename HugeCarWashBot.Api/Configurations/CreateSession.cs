﻿using System.Text;

namespace HugeCarWashBot.API.Configurations
{
    public class CreateSession
    {
        private readonly RequestDelegate _next;

        public CreateSession(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;

            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            //get body string here...
            var requestContent = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;  //rewinding the stream to 0
            await _next(httpContext);

        }
    }
}
