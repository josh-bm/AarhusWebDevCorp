using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Composing;
using Umbraco.Web;
using Umbraco.Web.Mvc;


namespace AarhusWebDevCoop.App_Code
{
    public class SetDefaultRenderMvcControllerComposer : IUserComposer 
    {
        // GET: SetDefaultRenderMvcControllerComposer
        public void Compose(Composition composition)
        {
            RouteTable.Routes.MapUmbracoRoute(
                "ProjectsCustomRoute",
                "all-projects/{status}",
                new
                {
                    controller = "ProjectsOverview",
                    action = "index",
                    status = UrlParameter.Optional
                },
                //Todo change number to correct
                new UmbracoVirtualNodeByIdRouteHandler(1186));
               
        }
    }
}