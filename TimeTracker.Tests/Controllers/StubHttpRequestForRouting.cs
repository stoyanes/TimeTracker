using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace TimeTracker.Tests.Controllers
{
    class StubHttpContextForRouting : HttpContextBase
    {
        readonly StubHttpRequestForRouting request;
        readonly StubHttpResponseForRouting response;

        public StubHttpContextForRouting(string appPath = "/", string requestUrl = "~/")
        {
            request = new StubHttpRequestForRouting(appPath, requestUrl);
            response = new StubHttpResponseForRouting();
        }

        public override HttpRequestBase Request
        {
            get { return request; }
        }

        public override HttpResponseBase Response
        {
            get { return response; }
        }
    }

    public class StubHttpRequestForRouting : HttpRequestBase
    {
        readonly string _appPath;
        readonly string _requestUrl;

        public StubHttpRequestForRouting(string appPath, string requestUrl)
        {
            _appPath = appPath;
            _requestUrl = requestUrl;
        }

        public override string ApplicationPath
        {
            get { return _appPath; }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get { return _requestUrl; }
        }

        public override string PathInfo
        {
            get { return ""; }
        }

        public override NameValueCollection ServerVariables
        {
            get { return new NameValueCollection(); }
        }
    }

    public class StubHttpResponseForRouting : HttpResponseBase
    {
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }
}
