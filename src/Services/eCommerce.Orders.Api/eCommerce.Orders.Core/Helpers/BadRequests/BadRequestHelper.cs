using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eCommerce.Orders.Core.Helpers.BadRequests
{
    public class BadRequestHelper
    {
        public static Dictionary<string, string[]> GetValidationResult(ModelStateDictionary modelState)
        {
            if (modelState.Keys.Any(k => k == ""))
            {
                IEnumerable<string> errors = modelState.Keys.Where(k => k == "")
                    .Select(k => modelState[k].Errors).First().Select(e => e.ErrorMessage);
                modelState.Remove("");
            }

            var errorList = modelState.Where(x => x.Value.Errors.Any()).ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            return errorList;
        }
    }
}
