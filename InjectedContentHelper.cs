using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Core.CMS.InjectedContent
{
    public static class InjectedContentHelper
    {
        private static Injected<ContentRootService> _contentRootService;

        public static ContentReference GetInjectedContentReference<T>()
            where T : PageData
        {
            return new InjectedContent<T>().ContentReference;
        }

        public static ContentReference GetInjectedContentReference<T>(Guid rootGuid)
            where T : PageData
        {
            return new InjectedContent<T>().ContentReference;
        }

        public static void RegisterInjectedContent<T>(ContentReference parentReference)
            where T : PageData
        {
            var cartPageContentRootId = InjectedContent<T>.GetContentRootPageGuid();
            var cartPageContentRootPageName = InjectedContent<T>.GetContentRootPageName();

            if (cartPageContentRootId.HasValue)
            {
                _contentRootService.Service.Register<T>(cartPageContentRootPageName, cartPageContentRootId.Value, parentReference);
            }
        }
    }
}
