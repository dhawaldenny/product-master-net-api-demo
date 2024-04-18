using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMaster.Business
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T Result { get; set; }
    }
}
