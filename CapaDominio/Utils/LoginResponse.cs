using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio.Utils
{
    public class LoginResponse<T>
    {
        public T value { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
        public string token { get; set; }
        public LoginResponse()
        {

            status = false;
            token = string.Empty;
            message = string.Empty;
            value = default(T);
        }
    }
}
