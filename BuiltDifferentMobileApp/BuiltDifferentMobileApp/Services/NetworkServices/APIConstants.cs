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

        /*
         *  WORKOUT URIS
         */

        public static string GetWorkoutByIdUri(int id) {
            return $"{BaseAddress}/workouts/{id}";
        }

        public static string GetWorkoutsUri() {
            return $"{BaseAddress}/workouts";
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

        public static string GetMealsByClientId(int id) {
            return $"{BaseAddress}/meals/client/{id}";
        }
    }
}
