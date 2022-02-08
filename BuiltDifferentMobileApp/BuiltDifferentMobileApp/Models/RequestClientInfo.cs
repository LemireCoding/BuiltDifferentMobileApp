using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class RequestClientInfo
    {
        public int id { get; set; }
        public string status { get; set; }
        public int coachId { get; set; }
        public int clientId { get; set; }
        public string clientName { get; set; }
        public string planSelected { get; set; }
        public DateTime approvalDate { get; set; }
        public DateTime requestDate { get; set; }

        public RequestClientInfo(int id, string status, int coachId, int clientId, string clientName, string planSelected, DateTime approvalDate, DateTime requestDate)
        {
            this.id = id;
            this.status = status;
            this.coachId = coachId;
            this.clientId = clientId;
            this.clientName = clientName;
            this.planSelected = planSelected;
            this.approvalDate = approvalDate;
            this.requestDate = requestDate;
        }
    }
}
