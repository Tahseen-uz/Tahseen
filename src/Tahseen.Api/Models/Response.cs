using Tahseen.Service.Helpers;

namespace Tahseen.Api.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }    
        public string PaginationData { get; set; }
        public void MapPaginationHeader()
        {
            if (HttpContextHelper.ResponseHeaders != null && HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
            {
                PaginationData = HttpContextHelper.ResponseHeaders["X-Pagination"];
            }
        }

    }
}
