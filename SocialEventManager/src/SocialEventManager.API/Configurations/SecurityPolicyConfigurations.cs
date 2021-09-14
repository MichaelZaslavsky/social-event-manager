using Microsoft.AspNetCore.Builder;

namespace SocialEventManager.API.Configurations
{
    public static class SecurityPolicyConfigurations
    {
        public static HeaderPolicyCollection GetPermissionPolicies()
        {
            return new HeaderPolicyCollection()
                .AddPermissionsPolicy(builder =>
                {
                    builder.AddAutoplay().Self();
                    builder.AddCamera().None();
                    builder.AddEncryptedMedia().Self();
                    builder.AddFullscreen().All();
                    builder.AddGeolocation().None();
                    builder.AddGyroscope().None();
                    builder.AddMagnetometer().None();
                    builder.AddMicrophone().None();
                    builder.AddMidi().None();
                    builder.AddPayment().None();
                    builder.AddPictureInPicture().None();
                    builder.AddSpeaker().None();
                    builder.AddSyncXHR().None();
                    builder.AddUsb().None();
                    builder.AddVR().None();
                });
        }
    }
}
