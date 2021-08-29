namespace SocialEventManager.Shared.Constants
{
    public static class ApiPathConstants
    {
        public const string Roles = Api + "roles";
        public const string Ready = "ready";
        public const string HealthReady = "/" + Health + Ready;
        public const string HealthLive = "/" + Health + "live";

        private const string Api = "api/";
        private const string Health = "health/";
    }
}
