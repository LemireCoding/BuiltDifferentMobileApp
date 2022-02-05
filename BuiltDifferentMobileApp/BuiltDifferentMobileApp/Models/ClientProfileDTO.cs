using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class ClientProfileDTO 
    {
        public string name { get; set; }
        public int userId { get; set; }
        public int currentWeight { get; set; }
        public int profilePictureId { get; set; }

        public ClientProfileDTO(string name, int userId, int currentWeight, int profilePictureId)
        {
            this.name = name;
            this.userId = userId;
            this.currentWeight = currentWeight;
            this.profilePictureId = profilePictureId;
        }

    }
}