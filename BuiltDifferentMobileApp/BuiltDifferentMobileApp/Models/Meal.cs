using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public class Meal {
        public int id { get; set; }
        public string mealName { get; set; }
        public int calories { get; set; }
        public int protein { get; set; }
        public double carbs { get; set; }
        public double fat { get; set; }
        public string recipe { get; set; }
        public int clientId { get; set; }
        public int coachId { get; set; }
        public string imageLink { get; set; }
        public string mealType { get; set; }
        public DateTime day { get; set; }
        public bool isEaten { get; set; }
    }
}
