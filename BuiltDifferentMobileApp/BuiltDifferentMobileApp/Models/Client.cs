using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public class Client : User {
        public override int id { get; set; }
        public override string name { get; set; }
        public override int userId { get; set; }
        public int coachId { get; set; }
        public bool waitingApproval { get; set; }
    }
}
