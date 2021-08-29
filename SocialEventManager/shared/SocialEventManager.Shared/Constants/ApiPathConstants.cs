namespace SocialEventManager.Shared.Constants
{
    public static class ApiPathConstants
    {
        public const string Roles = Api + "roles";
        public const string Ready = "ready";
        public const string HealthReady = "/" + Health + Ready;

        private const string Api = "api/";
        private const string Health = "health/";
    }
}
