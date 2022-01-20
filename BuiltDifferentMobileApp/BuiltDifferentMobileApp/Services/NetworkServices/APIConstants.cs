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
         *  COACH URIS
         *  All endpoints on the api are available (post delete update not listed here yet)
         */

        public static string GetAllCoaches()
        {
            return $"{BaseAddress}/coaches";
        }

        public static string GetAllVerifiedCoaches()
        {
            return $"{BaseAddress}/coaches/verified";
        }

        public static string GetAllAvailableCoach(string query)
        {
            return $"{BaseAddress}/coaches/available{query}";
        }

        public static string GetCoachByCoachId(int id)
        {
            return $"{BaseAddress}/coaches/{id}";
        }

        /*
         *  WORKOUT URIS
         */

        public static string PostWorkoutUri()
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/workouts";
        }

        public static string GetWorkoutsByClientId(int id)
        {
            return $"{BaseAddress}/workouts/client/{id}";
        }
        public static string GetWorkoutsByWorkoutId(int workoutId)
        {
            return $"{BaseAddress}/workouts/{workoutId}";
        }

        public static string UpdateWorkoutByWorkoutId(int workoutId)
        {
            return $"{BaseAddress}/workouts/{workoutId}";
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

        public static string PostMealUri()
        {
            return $"{BaseAddress}/meals";
        }
        public static string PutMealUri(int id)
        {
            return $"{BaseAddress}/meals/{id}";
        }

        public static string GetLoginUri() {
            return $"{BaseAddress}/login";
        }

        public static string PostForgotPasswordUri()
        {
            return $"{BaseAddress}/forgot-password";
        }

        public static string GetResetPasswordUri(string email, string token)
        {
            return $"{BaseAddress}/reset-password{email}/{token}";
        }

        public static string GetClientsForCoachId(int coachId) {
            return $"{BaseAddress}/coach/{coachId}/clients";
        }

        public static string GetRegisterNewAccountUri() {
            return $"{BaseAddress}/register";
        }

        public static string GetPendingCoachesUri() {
            return $"{BaseAddress}/coaches/pending";
        }

        public static string GetCoachByIdUri(int coachId) {
            return $"{BaseAddress}/coaches/{coachId}";
        }
        /*
         * Profile URI
         */
        public static string PutProfileUri(int userId)
        {
            return $"{BaseAddress}/profile/{userId}";
        }
        public static string GetClientProfileUri(int userId)
        {
            return $"{BaseAddress}/profile/{userId}";
        }


        public static string UploadCertificationUri(int coachId) {
            return $"{BaseAddress}/certification/upload/{coachId}";
        }

    }
}
