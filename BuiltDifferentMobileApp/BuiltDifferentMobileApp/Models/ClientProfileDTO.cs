using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    class ClientProfileDTO 
    {
        public string name { get; set; }
        public int userId { get; set; }
        public int currentWeight { get; set; }
        public string profilePicture { get; set; }

        public ClientProfileDTO(string name, int userId, int currentWeight, string profilePicture)
        {
            this.name = name;
            this.userId = userId;
            this.currentWeight = currentWeight;
            this.profilePicture = profilePicture;
        }
    }
}