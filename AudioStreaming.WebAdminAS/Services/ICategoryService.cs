using AudioStreaming.WebAdminAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> getCategory();
    }
}
