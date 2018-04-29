using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public interface IBaseUpdateServiceInterface<Z>
    {
        Task<Job> UpdateAsync(Guid Id, Z body);

    }
}
