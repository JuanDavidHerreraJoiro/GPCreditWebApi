using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(string message)
        {
            Successed = false;
            Message = message;
        }

        public Response(T data, string? message = null)
        {
            Successed = true;
            Message = message;
            Data = data;
        }

        public bool Successed { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }
    }
}
