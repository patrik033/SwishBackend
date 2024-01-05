using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitCommons.Common.Email
{ 
    public class EmailConfirmationMessage
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailToken { get; set; }

    }
}
