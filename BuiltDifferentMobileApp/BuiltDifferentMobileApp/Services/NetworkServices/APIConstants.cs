using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.NetworkServices
{
    public static class APIConstants
    {
        public static readonly string HOST = Device.RuntimePlatform == Device.Android ? "10.0.2.2" : "localhost";
        private static readonly string PORT = "8080";


        public static string PostWorkoutUri()
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/api/workouts";
        }

        public static string GetWorkoutsByClientId(int id) {
            return $"{BaseAddress}/workouts/client/{id}";
        }

        /*
         *  MEAL URIS
         */

        public static string GetMealByIdUri(int id) {
            return $"{BaseAddress}/meals/{id}";
        }

        public static string GetMealsUri() {
            return $"{BaseAddress}/meals";
        }


        public static string GetWorkoutsByClientId(int clientId)
        {
            return $"{BaseAddress}/api/workouts/client/{clientId}";
        }

        public static string GetWorkoutsByWorkoutId(int workoutId)
        {
            return $"{BaseAddress}/api/workouts/{workoutId}";
        }

        public static string UpdateWorkoutByWorkoutId(int workoutId)
        {
            return $"{BaseAddress}/api/workouts/{workoutId}";
        }
    }
}
