using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASS_PRC.Services.Services
{
    public interface ICategoryServices
    {
        Task<List<Data.Entity.Category>> getCategory();
    }
}
