using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryAdmin2.Utils
{
    public class UrlBuilder
    {
        public UrlBuilder(Controller context, string action, string controller)
        {
            url.Append(context.Url.Action(action, controller, null, context.Request.Url.Scheme) + "?");
        }

        private StringBuilder url = new StringBuilder();

        public void AppendParam(string key, object value)
        {
            url.Append(key);
            url.Append("=");
            url.Append(HttpContext.Current.Server.UrlEncode(value.ToString()));
            url.Append("&");
        }

        public override string ToString()
        {
 	         return url.ToString();
        }
    }
}