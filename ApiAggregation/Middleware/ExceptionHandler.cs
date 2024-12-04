namespace ApiAggregationProject.Api.Middleware
{
    public class ExceptionHelper
    {
        private readonly RequestDelegate _next;

        public ExceptionHelper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"An error occured: {ex.Message}");
            }
        }
    }
}
