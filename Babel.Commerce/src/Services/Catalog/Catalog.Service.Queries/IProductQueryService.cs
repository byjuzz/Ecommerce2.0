using Catalog.Service.Queries.DTO;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Service.Queries
{
    public  interface IProductQueryService
    {
        Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null);
        Task<ProductDto> GetAsync(int id);
    }
}
