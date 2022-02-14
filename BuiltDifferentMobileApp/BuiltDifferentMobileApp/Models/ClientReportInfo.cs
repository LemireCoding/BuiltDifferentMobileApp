using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class ClientReportInfo
    {
        public int clientId { get; set; }
        public string clientName { get; set; }
        public int coachId { get; set; }
        public string coachName { get; set; }
        public double bmi { get; set; }
        public double caloriesConsumed { get; set; }
        public int workoutsDone { get; set; }
    }
}
