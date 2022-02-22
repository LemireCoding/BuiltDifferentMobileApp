using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class ClientWithProgress : User
    {
        public override int id { get; set; }
        public override string name { get; set; }
        public override int userId { get; set; }
        public int coachId { get; set; }
        public bool waitingApproval { get; set; }
        public int startWeight { get; set; }
        public int currentWeight { get; set; }
        public int profilePictureId { get; set; }
        public string percentage { get; set; }
        public string profilePicture { get; set; }

        public ClientWithProgress(int id, string name, int userId, int coachId, bool waitingApproval, int startWeight, int currentWeight, int profilePictureId, string percentage)
        {
            this.id = id;
            this.name = name;
            this.userId = userId;
            this.coachId = coachId;
            this.waitingApproval = waitingApproval;
            this.startWeight = startWeight;
            this.currentWeight = currentWeight;
            this.profilePictureId = profilePictureId;
            this.percentage = percentage;
            profilePicture = APIConstants.GetProfilePictureUri(userId);
        }
    }
}
