using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class Request
    {
        public int id { get; set; }
        public string status { get; set; }
        public int coachId { get; set; }
        public int clientId { get; set; }
        public DateTime approvalDate { get; set; }
        public DateTime requestDate { get; set; }
    }
}
