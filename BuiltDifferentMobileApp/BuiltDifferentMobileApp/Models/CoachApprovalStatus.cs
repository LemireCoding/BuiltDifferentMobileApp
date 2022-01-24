using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public class CoachApprovalStatus {
        public string approvalStatus { get; set; }

        public const string APPROVED = "APPROVED";
        public const string PENDING = "PENDING";
        public const string DENIED = "DENIED";
    }
}
