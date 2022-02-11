using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class ClientRemoveDTO
    {
        public int clientId { get; set; }
        public int coachId { get; set; }

        public ClientRemoveDTO(int clientId, int coachId)
        {
            this.clientId = clientId;
            this.coachId = coachId;
        }
    }
}
