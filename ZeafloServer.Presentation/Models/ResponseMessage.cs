using System.Text.Json.Serialization;

namespace ZeafloServer.Presentation.Models
{
    public sealed class ResponseMessage<T>
    {
        [JsonPropertyName("success")] public bool IsSuccess { get; init; }

        [JsonPropertyName("errors")] public IEnumerable<string>? Errors { get; init; }

        [JsonPropertyName("detailedErrors")]
        public IEnumerable<DetailedError> DetailedErrors { get; init; } = Enumerable.Empty<DetailedError>();

        [JsonPropertyName("data")] public T? Data { get; init; }
    }
}
