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

        private static readonly string BaseAddress = $"http://{HOST}:{PORT}/api";

        public static string PostWorkoutUri()
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/workouts";
        }

        public static string GetWorkoutsByClientId(int id)
        {
            return $"{BaseAddress}/workouts/client/{id}";
        }

        /*
         *  MEAL URIS
         */

        public static string GetMealByIdUri(int id)
        {
            return $"{BaseAddress}/meals/{id}";
        }

        public static string GetMealsUri()
        {
            return $"{BaseAddress}/meals";
        }

        public static string GetMealsByClientId(int id)
        {
            return $"{BaseAddress}/meals/client/{id}";
        }

        public static string GetWorkoutsByWorkoutId(int workoutId)
        {
            return $"{BaseAddress}/workouts/{workoutId}";
        }

        public static string UpdateWorkoutByWorkoutId(int workoutId)
        {
            return $"{BaseAddress}/workouts/{workoutId}";
        }

        public static string PostMealUri()
        {
            return $"{BaseAddress}/meals";
        }
        public static string PutMealUri(int id)
        {
            return $"{BaseAddress}/meals/{id}";
        }
    }
}
