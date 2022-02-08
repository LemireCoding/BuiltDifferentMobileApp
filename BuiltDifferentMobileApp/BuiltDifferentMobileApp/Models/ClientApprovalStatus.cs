using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class ClientApprovalStatus
    {
        public string status { get; set; }

        public ClientApprovalStatus(string status)
        {
            this.status = status;
        }
    }
}
