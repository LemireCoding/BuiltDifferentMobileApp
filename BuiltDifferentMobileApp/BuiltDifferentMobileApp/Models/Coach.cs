using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public class Coach : User {
        public override int id { get; set; }
        public override string name { get; set; }
        public override int userId { get; set; }
        public string type { get; set; }
        public bool isAvailable { get; set; }
        public bool offersMeal { get; set; }
        public bool offersWorkout { get; set; }
        public int certificationId { get; set; }
        public string gender { get; set; }
        public bool isVerified { get; set; }
        public string description { get; set; }
        public double pricing { get; set; }
        public int profilePictureId{ get; set; }
        public string payPalLink { get; set; }
    }
}
