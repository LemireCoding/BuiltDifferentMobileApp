using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public class Workout {
        public int workoutId { get; set; }
        public int coachId { get; set; }
        public int clientId { get; set; }
        public string workoutType { get; set; }
        public string workoutName { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public int weight { get; set; }
        public int duration { get; set; }
        public int restTime { get; set; }
        public DateTime day { get; set; }

        //Optional fields below
        public string description { get; set; }
        public bool isCompleted { get; set; }
        public string videoLink { get; set; }

        public Workout(int workoutId, int coachId, int clientId, string workoutType, string workoutName, int sets, 
            int reps, int weight, int duration, int restTime, DateTime day, string description, bool isCompleted, string videoLink)
        {
            this.workoutId = workoutId;
            this.coachId = coachId;
            this.clientId = clientId;
            this.workoutType = workoutType;
            this.workoutName = workoutName;
            this.sets = sets;
            this.reps = reps;
            this.weight = weight;
            this.duration = duration;
            this.restTime = restTime;
            this.day = day;
            this.description = description;
            this.isCompleted = isCompleted;
            this.videoLink = videoLink;
        }
    }
}
