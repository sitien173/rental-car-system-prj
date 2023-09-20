using Microsoft.AspNetCore.Mvc;
using Skoruba.IdentityServer4.Admin.UI.Configuration;

namespace Skoruba.IdentityServer4.Admin.UI.ViewComponents
{
	public class IdentityServerLinkViewComponent : ViewComponent
    {
        private readonly AdminConfiguration _configuration;

        public IdentityServerLinkViewComponent(AdminConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var identityServerUrl = _configuration.IdentityServerBaseUrl;
            
            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(model: identityServerUrl);
        }
    }
}
