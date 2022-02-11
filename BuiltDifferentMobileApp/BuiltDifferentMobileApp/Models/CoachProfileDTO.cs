using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class CoachProfileDTO
    {
        public string name { get; set; }
        public int userId { get; set; }
        public string type { get; set; }
        public bool isAvailable { get; set; }
        public bool offersMeal { get; set; }
        public bool offersWorkout { get; set; }
        public int certificationId { get; set; }
        public string gender { get; set; }
        public bool isVerified { get; set; }
        public string description { get; set; }
        public double pricing { get; set; }
        public int profilePictureId { get; set; }
        public string payPalLink { get; set; }

        public CoachProfileDTO(string name, int userId, string type, bool isAvailable, bool offersMeal, bool offersWorkout, int certificationId, string gender, bool isVerified, string description, double pricing, int profilePictureId, string payPalLink)
        {
            this.name = name;
            this.userId = userId;
            this.type = type;
            this.isAvailable = isAvailable;
            this.offersMeal = offersMeal;
            this.offersWorkout = offersWorkout;
            this.certificationId = certificationId;
            this.gender = gender;
            this.isVerified = isVerified;
            this.description = description;
            this.pricing = pricing;
            this.profilePictureId = profilePictureId;
            this.payPalLink = payPalLink;
        }

    }
}
