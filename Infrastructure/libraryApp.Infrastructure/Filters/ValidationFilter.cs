using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace libraryApp.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid) // gecerrsiz ise
            {
                var errors = context.ModelState.Where(x => x.Value.Errors.Any())
                   .ToDictionary(e => e.Key, e => e.Value.Errors.Select(x => x.ErrorMessage))
                   .ToArray();


                context.Result = new BadRequestObjectResult(errors);

                return;
            }

            await next();
        }
    }
}
