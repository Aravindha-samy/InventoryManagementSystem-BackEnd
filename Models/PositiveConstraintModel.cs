namespace InventoryManagementSystem.Models
{
    public class PositiveConstraintModel:IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if(values.TryGetValue(routeKey, out var value) && value != null)
            {
                if(int.TryParse(value.ToString(), out int intValue))
                {
                    return intValue > 0;
                }
            }
            return false;
        }
    }
}


