using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Restaurant.System.Controllers.Controllers.Base;

public sealed class ApiRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (!typeof(ApiControllerBase).IsAssignableFrom(controller.ControllerType))
        {
            return;
        }

        foreach (var selector in controller.Selectors)
        {
            if (selector.AttributeRouteModel is null)
            {
                continue;
            }

            selector.AttributeRouteModel =
                AttributeRouteModel.CombineAttributeRouteModel(
                    new AttributeRouteModel(
                        new RouteAttribute("api/v{version:apiVersion}")
                    ),
                    selector.AttributeRouteModel);
        }
    }
}