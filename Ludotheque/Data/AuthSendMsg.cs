using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Data
{
    /// <summary>
    /// Class for sending mails
    /// </summary>
    public class AuthSendMsg
    {
        public string SendGridUser { get; set; } 
        public string SendGridKey { get; set; }
    }
}
