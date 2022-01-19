using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class ClientProfileRecieveDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int userId { get; set; }
        public int coachId { get; set; }
        public bool waitingApproval { get; set; }
        public int startWeight { get; set; }
        public int currentWeight { get; set; }
        public string profilePicture { get; set; }
    }
}
