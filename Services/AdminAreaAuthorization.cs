using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace RclubHook.Services;

public class AdminAreaAuthorization: IControllerModelConvention

{
    private readonly string _area;
    private readonly string _policy;

    public AdminAreaAuthorization(string area, string policy)
    {
        _area = area;
        _policy = policy;
    }

    public void Apply(ControllerModel controller)
    {
        // Проверяем атрибуты Area
        var areaAttribute = controller.Attributes
            .OfType<AreaAttribute>()
            .FirstOrDefault(a => a.RouteValue.Equals(_area, StringComparison.OrdinalIgnoreCase));

        // Проверяем route values
        var hasArea = controller.RouteValues
            .Any(r => r.Key.Equals("area", StringComparison.OrdinalIgnoreCase) &&
                      r.Value?.Equals(_area, StringComparison.OrdinalIgnoreCase) == true);

        if (areaAttribute != null || hasArea)
        {
            controller.Filters.Add(new AuthorizeFilter(_policy));
        }
    }
}