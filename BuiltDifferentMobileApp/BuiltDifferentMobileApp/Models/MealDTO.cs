using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class MealDTO
    {
        public int clientId { get; set; }
        public int coachId { get; set; }
        public string mealName { get; set; }
        public string mealType { get; set; }
        public double calories { get; set; }
        public double protein { get; set; }
        public double carbs { get; set; }
        public double fat { get; set; }
        public string recipe { get; set; }
        public string imageLink { get; set; }
        public DateTime day { get; set; }
        public bool isEaten { get; set; }

        public MealDTO(int clientId, int coachId, string mealName, string mealType, double calories, double protein, double carbs, double fat, string recipe, string imageLink, DateTime day, bool isEaten)
        {
            this.clientId = clientId;
            this.coachId = coachId;
            this.mealName = mealName;
            this.mealType = mealType;
            this.calories = calories;
            this.protein = protein;
            this.carbs = carbs;
            this.fat = fat;
            this.recipe = recipe;
            this.imageLink = imageLink;
            this.day = day;
            this.isEaten = isEaten;
        }
    }
}
