using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.NetworkServices
{
    public static class APIConstants
    {
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "http://10.0.2.2:8080" : "http://localhost:8080";

        public static string PostWorkoutUri()
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/api/workouts";
        }
        public static string GetWorkoutsUri(int clientId)
        {
            //will need to add client id , etc...

            return $"{BaseAddress}/api/workouts/client/{clientId}";
        }
        public static string PutWorkoutUri(int workoutId)
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/api/gateway/workouts/{workoutId}";

            return $"{BaseAddress}/api/workouts";
        }
        public static string GetMealsByClientId(int clientId)
        {
            return $"{BaseAddress}/api/meals/client/{clientId}";
        }

        internal static string GetWorkoutsByClientId(int clientId)
        {
            return $"{BaseAddress}/api/workouts/client/{clientId}";
        }
    }
}
