using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mail
{
     public class letter
    {
        public letter(MimeMessage message)
        {
            To = message.To.ToString();
            From= message.From.ToString();
            Subject= message.Subject;
            Body = new string(message.TextBody.Take(10).ToArray());
            dateTime = message.Date.DateTime;
        }

        public string To { get; set; }
        public string From { get; set; }    
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime dateTime { get; set; }
    }
}
