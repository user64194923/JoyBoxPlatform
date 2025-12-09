
namespace Microsoft.OpenApi
{
    internal class Models
    {
        public static object JsonObjectType { get; internal set; }

        internal class OpenApiInfo : OpenApi.OpenApiInfo
        {
            public string Version { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }
}