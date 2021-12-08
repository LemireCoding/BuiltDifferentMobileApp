using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace BuiltDifferentMobileApp.Services.NetworkServices
{
    public static class APIConstants
    {
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:80" : "http://localhost:8080";

        public static string PostWorkoutUri()
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/api/gateway/workouts";
        }
        public static string GetWorkoutsUri()
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/api/gateway/workouts";
        }
        public static string PutWorkoutUri(int workoutId)
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/api/gateway/workouts/{workoutId}";
        }
    }
}
