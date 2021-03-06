using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.NetworkServices
{
    public static class APIConstants
    {
#if DEBUG
        public static readonly string PROTOCOL = "http";
        public static readonly string HOST = Device.RuntimePlatform == Device.Android ? "10.0.2.2" : "localhost";
        private static readonly string PORT = ":8080";
#else
        public static readonly string PROTOCOL = "https";
        public static readonly string HOST = "built-different-api.herokuapp.com";
        private static readonly string PORT = "";
#endif

        private static readonly string BaseAddress = $"{PROTOCOL}://{HOST}{PORT}/api";


        public static string GetAllRequestsByClient(int id)
        {
            return $"{BaseAddress}/requests/client/{id}";
        }

        public static string DeleteRequestURI(int id)
        {
            return $"{BaseAddress}/requests/{id}";
        }
        public static string PostRequestURI()
        {
            //will need to add client id , etc...
            return $"{BaseAddress}/requests";
        }

        /*
        * Coach Client Accept/Deny URI
        */

        public static string UpdateApproveDenyRequestUri(int clientId)
        {
            return $"{BaseAddress}/requests/applications/{clientId}";
        }

        public static string GetPendingRequestsUri(int coachId)
        {
            return $"{BaseAddress}/requests/coach/pending/{coachId}";
        }


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

        public static string UpdateClientRemoveFromServiceUri(int clientId, int coachId)
        {
            return $"{BaseAddress}/coaches/{coachId}/clients/remove/{clientId}";
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
        public static string GetProfileUri()
        {
            return $"{BaseAddress}/profile";
        }

        //TEST BEFORE MERGE!

        public static string PutProfileUri()
        {
            return $"{BaseAddress}/profile";
        }

        public static string GetClientProfileUri(int userId)

        {
            return $"{BaseAddress}/profile";
        }

        public static string PostUploadProfilePicture()
        {
            return $"{BaseAddress}/profile/picture/upload";
        }

        public static string GetProfilePictureUri(int userId = 0)
        {
            return userId == 0 ? $"{BaseAddress}/profile/picture" : $"{BaseAddress}/profile/picture/{userId}";
        }

        public static string UploadCertificationUri() {
            return $"{BaseAddress}/certification/upload";
        }

        public static string GetCoachCertificationUri(int coachId) {
            return $"{BaseAddress}/certification/{coachId}";
        }

        public static string GetCoachApprovalStatusUri(int coachId) {
            return $"{BaseAddress}/certification/{coachId}/status";
        }

        public static string GetApproveDenyCoachUri(int coachId) {
            return $"{BaseAddress}/admin/applications/{coachId}";
        }

        public static string GetUserAccountStatusUri(int userId) {
            return $"{BaseAddress}/admin/user/{userId}/accountStatus";
        }

        public static string GetUsersCreatedBetweenRange(DateTime startDate, DateTime endDate) {
            return $"{BaseAddress}/admin/stats/users-registered?start={startDate.ToString("MM dd yyyy").Replace(' ', '-')}&end={endDate.ToString("MM dd yyyy").Replace(' ', '-')}";
        }

        public static string GetCertificationStatusesCreatedBetweenRange(DateTime startDate, DateTime endDate) {
            return $"{BaseAddress}/certifications?start={startDate.ToString("MM dd yyyy").Replace(' ', '-')}&end={endDate.ToString("MM dd yyyy").Replace(' ', '-')}";
        }

        public static string GetLogo(bool loop = false) {
            return loop ? $"{BaseAddress}/logo-loop" : $"{ BaseAddress}/logo";
        }

        public static string PostUploadProfileBannerPicture() {
            return $"{BaseAddress}/profile/banner/upload";
        }

        public static string GetProfileBannerPictureUri(int userId) {
            return $"{BaseAddress}/profile/banner/{userId}";
        }

        public static string GetUserJoinDate(int userId) {
            return $"{BaseAddress}/user/{userId}/join-date";
        }

    }
}
