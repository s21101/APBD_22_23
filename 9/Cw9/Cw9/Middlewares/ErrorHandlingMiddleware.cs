namespace Cw9.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        { 
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); 
            }
            catch (Exception e)
            {
                var path = Path.GetFullPath(".");

                if (!Directory.Exists(path+"\\Logs"))
                {
                    Directory.CreateDirectory("\\Logs");
                }

                
                if (!File.Exists(path + "\\Logs\\logs.txt"))
                {
                    File.Create(path+"\\Logs\\logs.txt");
                }
                

                using (StreamWriter sw = new(path + "\\Logs\\logs.txt"))
                {
                    await sw.WriteLineAsync(e.ToString());
                    //await sw.WriteLineAsync(e.Data.ToString());
                }
                


                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync("Unexpected problem " );
            }

        }
    }
}
