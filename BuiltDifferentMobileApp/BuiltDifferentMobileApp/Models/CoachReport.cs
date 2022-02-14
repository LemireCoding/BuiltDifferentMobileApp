using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class CoachReport
    {
        public string coachName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int numberOfClients { get; set; }
        public List<ClientReportInfo> clientReports { get; set; }
    }
}
