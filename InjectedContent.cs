using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Licensing.Services;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using PersonalBlog.Core.CMS.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.Core.CMS.InjectedContent
{
    public struct InjectedContent<T> where T : ContentData
    {
        #region Private

        private string _contentRootName => GetContentRootPageName();
        private ContentReference _contentReference;
        private T _content;

        #endregion

        public ContentReference ContentReference
            =>
                _contentReference.IsNullOrEmpty()
                    ? _contentReference = _contentRootService.Service.Get(_contentRootName)
                    : _contentReference;

        public T Content => _content ?? (_content = _contentLoader.Service.Get<T>(ContentReference));
        
        #region Static

        private static Injected<ContentRootService> _contentRootService;
        private static Injected<IContentLoader> _contentLoader;

        public static Guid? GetContentRootPageGuid()
        {
            var rootPageGuidString = GetInjectedPageConfiguration<T>()?.RootPageGuid;
            Guid parsedGuid;

            if (Guid.TryParse(rootPageGuidString, out parsedGuid))
                return parsedGuid;

            return null;
        }

        public static string GetContentRootPageName()
        {
            return GetInjectedPageConfiguration<T>()?.RootPageName;
        }

        private static InjectedContentAttribute GetInjectedPageConfiguration<TType>()
        {
            return typeof(TType).GetCustomAttribute<InjectedContentAttribute>();
        }

        #endregion

    }
}
