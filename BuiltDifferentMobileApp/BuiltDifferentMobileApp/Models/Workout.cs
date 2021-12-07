using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.Models {
    public class Workout {
        public int exerciseId { get; set; }
        public string trainingType { get; set; }
        public string difficulty { get; set; }
        public int duration { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public int restTime { get; set; }
        public string additionalComments { get; set; }
        public string videoLink { get; set; }
        public bool isCompleted { get; set; }
        public DateTime day { get; set; }

    }
}
