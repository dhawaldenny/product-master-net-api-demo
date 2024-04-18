using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductMaster.Business
{
    public class APIException : Exception
    {
        public HttpStatusCode _statusCode { get; set; }
        public APIException(string message, HttpStatusCode statusCode) : base(message)
        {
            this._statusCode = statusCode;
        }

    }
}
