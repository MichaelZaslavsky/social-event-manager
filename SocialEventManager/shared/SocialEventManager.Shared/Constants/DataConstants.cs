using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Shared.Constants
{
    public static class DataConstants
    {
        public const string SerilogUrl = "http://seq-analysis:5341";
        public const string RandomText = "Random Text";
        public const string PhoneNumber = "+972 123456789";
        public const string RepositoryTests = "RepositoryTests";
        public const string LoremIpsum = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
        public const string Password = "Password";

        public static string Length256 => RandomGeneratorHelpers.GenerateRandomValue(256);
    }
}
