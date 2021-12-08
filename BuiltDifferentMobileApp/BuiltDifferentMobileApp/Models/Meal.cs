using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public class Meal {
        public int MealId { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
        public string Recipe { get; set; }
        public int ClientId { get; set; }
        public int CoachId { get; set; }
        public string ImageLink { get; set; }
        public string MealType { get; set; }
        public DateTime Day { get; set; }
        public bool isEaten { get; set; }
    }
}
