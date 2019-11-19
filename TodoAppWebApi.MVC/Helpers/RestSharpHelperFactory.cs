using Ertglr.Libraries.Common.Helpers;

namespace TodoAppWebApi.MVC.Helpers
{
    public static class RestSharpHelperFactory
    {
        public static RestSharpHelper TodoApiHelper { get; } = new RestSharpHelper("ApiEndPoint");
    }
}