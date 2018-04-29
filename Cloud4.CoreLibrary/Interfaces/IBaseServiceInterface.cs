using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public interface IBaseServiceInterface<T>
    {
        Task<List<T>> AllAsync();

        Task<T> GetAsync(Guid Id);


        Task<Job> DeleteAsync(Guid Id, bool Wait);

    }
}
