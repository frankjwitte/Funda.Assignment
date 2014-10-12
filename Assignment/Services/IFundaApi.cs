using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public interface IFundaApi
    {
        IEnumerable<Models.Object> Query(string searchType, string query, int pageSize);
        IEnumerable<Models.MakelaarInfo> GetTop10(string searchType, string query);
    }
}
