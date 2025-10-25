using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication17.Models;

namespace WebApplication17.Filters
{
    // يتحقق أن Location تكون USA أو EG
    public class LocationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Values.FirstOrDefault() is Department dept)
            {
                var loc = (dept.Location ?? string.Empty).Trim();
                if (!string.Equals(loc, "USA", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(loc, "EG", StringComparison.OrdinalIgnoreCase))
                {
                    context.Result = new BadRequestObjectResult(new { error = "Location must be either 'USA' or 'EG'" });
                }
            }
        }
    }
}
