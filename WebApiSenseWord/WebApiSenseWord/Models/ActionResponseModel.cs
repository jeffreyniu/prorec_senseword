using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiSenseWord.Models
{
    public class ActionResponseModel
    {
        public HttpStatusCode Status { get; set; }

        public string Error { get; set; }

        public string Data { get; set; }
    }
}
