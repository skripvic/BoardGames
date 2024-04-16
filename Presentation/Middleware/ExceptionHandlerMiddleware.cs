using BuisinessLogic.Exceptions;

namespace Presentation.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedException e)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(Error(e));
            }
            catch (EntityNotFoundException e)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(Error(e));
            }
            catch (BadRequestException e)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(Error(e));
            }
            catch (Exception e)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(ErrorExtended(e));
            }
        }

        private static object Error(Exception e)
        {
            return new
            {
                Message = e.Message
            };
        }

        private static object ErrorExtended(Exception e)
        {
            return new
            {
                Message = e.Message,
                StackTrace = e.StackTrace
            };
        }
    }
}
