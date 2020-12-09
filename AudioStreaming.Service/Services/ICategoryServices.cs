using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public interface ICategoryServices
    {
        Task<List<Data.Entity.Category>> getCategory();
    }
}
