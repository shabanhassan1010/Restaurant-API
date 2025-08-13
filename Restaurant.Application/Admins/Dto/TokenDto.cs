using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Admins.Dto
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public string UserName { get; set; }
    }
}
