using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TTGS.Shared.Helper
{
    public static class HttpHeaderHelper
    {
        public static void AddPagingMetaHeader<T>(HttpResponse response, PagedList<T> pagedList) where T : class
        {
            var metadata = new
            {
                pagedList.TotalCount,
                pagedList.PageSize,
                pagedList.CurrentPage,
                pagedList.TotalPages,
                pagedList.HasNext,
                pagedList.HasPrevious
            };
            response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        }
    }
}
