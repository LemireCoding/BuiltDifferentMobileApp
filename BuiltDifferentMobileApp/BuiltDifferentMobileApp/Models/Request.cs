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
        public string coachName { get; set; }

        public Request(int id, string status, int coachId, int clientId, DateTime approvalDate, DateTime requestDate,string coachName)
        {
            this.id = id;
            this.status = status;
            this.coachId = coachId;
            this.clientId = clientId;
            this.approvalDate = approvalDate;
            this.requestDate = requestDate;
            this.coachName = coachName;
        }


    }
}
