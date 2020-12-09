using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public interface IBrandsService
    {
        Task<IList<DTO.Brand>> GetBrands(bool isSort, bool isDecending, bool isPaging, int page, int pageLimitItem, string orderBy, string searchKey);

    }
}
