using Google.Cloud.SecretManager.V1;

namespace Services
{
    public static class GoogleCloudService
    {
        public static string GetDiscordSecret()
        {
            // Build the environment variables..
            var projectId = Environment.GetEnvironmentVariable("projectId");
            var secretId = Environment.GetEnvironmentVariable("secretId");
            var secretVersionId = Environment.GetEnvironmentVariable("secretVersionId");

            // Create the client.
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();
            SecretVersionName secretVersionName = new SecretVersionName(projectId, secretId, secretVersionId);

            // Call the API.
            AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

            // Convert the payload to a string. Payloads are bytes by default.
            String payload = result.Payload.Data.ToStringUtf8();
            return payload;
        }
    }
}
