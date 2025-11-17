using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PE_PRN232_FA25_PhamThiThanhNgan.API.Commons
{
    public class ValidateModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .Select(e => new
                    {
                        Field = e.Key,
                        Message = e.Value!.Errors.First().ErrorMessage
                    })
                    .Select(e => $"{e.Field}: {e.Message}")
                    .ToList();

                var errorResponse = ErrorResponse.InvalidInput(string.Join("; ", errors));

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
