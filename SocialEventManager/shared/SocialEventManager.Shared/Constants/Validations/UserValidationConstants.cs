namespace SocialEventManager.Shared.Constants.Validations
{
    public static class UserValidationConstants
    {
        public static string CouldNotDeleteUser(string email) =>
            $"Could not delete user {email}.";

        public static string CouldNotInsertUser(string email) =>
            $"Could not insert user {email}.";

        public static string CouldNotUpdateUser(string email) =>
            $"Could not update user {email}.";
    }
}
