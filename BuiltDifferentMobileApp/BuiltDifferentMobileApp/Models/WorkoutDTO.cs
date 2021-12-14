using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models
{
    public class WorkoutDTO
    {
        public int id { get; set; }
        public int coachId { get; set; }
        public int clientId { get; set; }
        public string workoutType { get; set; }
        public string workoutName { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public int duration { get; set; }
        public int restTime { get; set; }
        public DateTime day { get; set; }
        public string description { get; set; }
        public bool isCompleted { get; set; }
        public string videoLink { get; set; }



        public WorkoutDTO(int id, int coachId, int clientId, string workouttype, string workoutname, int sets, int reps, int duration, int resttime, DateTime day, string desc, bool isCompleted, string videoLink)
        {
            this.id = id;
            this.coachId = coachId;
            this.clientId = clientId;
            this.workoutName = workoutname;
            this.workoutType = workouttype;
            this.sets = sets;
            this.reps = reps;
            this.duration = duration;
            this.restTime = resttime;
            this.day = day;
            this.description = desc;
            this.isCompleted = isCompleted;
            this.videoLink = videoLink;

        }
    }
}
