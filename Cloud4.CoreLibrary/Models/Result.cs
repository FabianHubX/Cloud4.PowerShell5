using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class Result
    {
        public Job Job { get; set; }

        public System.Net.HttpStatusCode Code { get; set; }

        public ErrorDetails Error { get; set; }
    }

    public class Result<T>
    {
        public Job Job { get; set; }

        public System.Net.HttpStatusCode Code { get; set; }

        public ErrorDetails Error { get; set; }

        public T Object { get; set; }
    }
}
