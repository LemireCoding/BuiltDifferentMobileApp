using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class RequestDTO
    {
        public string status { get; set; }
        public int coachId { get; set; }
        public int clientId { get; set; }
        public string clientName { get; set; }
        public string planSelected { get; set; }

        public RequestDTO (string status , int coachId, int clientId,string clientName, string planSelected)
        {
            this.status = status;
            this.coachId = coachId;
            this.clientId = clientId;
            this.clientName = clientName;
            this.planSelected = planSelected;
        }
     
    }
}
