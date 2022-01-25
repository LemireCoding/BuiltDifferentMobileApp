using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public class CoachApprovalStatus {
        public string approvalStatus { get; set; }

        public CoachApprovalStatus(string approvalStatus) {
            this.approvalStatus = approvalStatus;
        }
    }
}
