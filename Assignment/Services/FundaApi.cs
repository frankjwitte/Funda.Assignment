using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class FundaApi : IFundaApi
    {
        // Decompositie van URL.
        private readonly string baseurl_xml = "http://partnerapi.funda.nl/feeds/Aanbod.svc/";
        private readonly string baseurl_json = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/";
        private readonly string key = "005e7c1d6f6c4f9bacac16760286e3cd";
        private readonly int pageSize = 100;

        private readonly RestSharp.RestClient client;

        public FundaApi()
        {
            // Setup RestClient to JSON service endpoint
            client = new RestSharp.RestClient(baseurl_json + "/" + key + "/");
        }

        /// <summary>
        /// Query service and retrieve the objects in a paged manner.
        /// </summary>
        /// <param name="searchType">Koop/Huur/??</param>
        /// <param name="query">Query string '/city/specifics'</param>
        /// <param name="pageSize">Chunk size</param>
        /// <returns></returns>
        public IEnumerable<Models.Object> Query(string searchType, string query, int pageSize)
        {
            var result = new List<Models.Object>();
            int page = 1;

            while (true)
            {
                var resource = string.Format("?type={0}&zo={1}&page={2}&pagesize={3}",
                                            searchType, query, page, pageSize);
                var request = new RestSharp.RestRequest(resource, RestSharp.Method.GET);
                request.RequestFormat = RestSharp.DataFormat.Json;
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // If we have result, deserialize what we want to know. 
                    JsonDeserializer deserializer = new JsonDeserializer();
                    var sr = deserializer.Deserialize<Models.SearchResult>(response);
                    result.AddRange(sr.Objects);

                    // Did we get all pages yet?
                    if (sr.Paging.HuidigePagina < sr.Paging.AantalPaginas)
                    {
                        page++;
                        continue;
                    }

                }
                break;
            }
            return result;
        }

        /// <summary>
        /// Get top 10 of MakelaarInfo (id,name, object count) for specified query.
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Models.MakelaarInfo> GetTop10(string searchType, string query)
        {
            var queryResult = Query(searchType, query, pageSize);
            var result = from obj in queryResult
                         group obj by obj.MakelaarId into g
                         select new Assignment.Models.MakelaarInfo
                         {
                             MakelaarId = g.Key,
                             MakelaarNaam = g.First().MakelaarNaam,
                             ObjectCount = g.ToList().Count()
                         };
            return result.OrderByDescending(x => x.ObjectCount).Take(10);
        }
    }
}
