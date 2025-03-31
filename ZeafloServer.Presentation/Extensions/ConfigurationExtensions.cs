using ZeafloServer.Domain.Settings;

namespace ZeafloServer.Presentation.Extensions
{
    public static class ConfigurationExtensions
    {
        public static RabbitMqConfiguration GetRabbitMqConfiguration(
            this IConfiguration configuration)
        {
            var rabbitHost = configuration["RabbitMQ:Host"];
            var rabbitPort = configuration["RabbitMQ:Port"];
            var rabbitUser = configuration["RabbitMQ:Username"];
            var rabbitPass = configuration["RabbitMQ:Password"];

            return new RabbitMqConfiguration()
            {
                Host = rabbitHost ?? "",
                Port = int.Parse(rabbitPort ?? "0"),
                Username = rabbitUser ?? "",
                Password = rabbitPass ?? ""
            };
        }
    }
}
