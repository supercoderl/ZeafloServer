using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Deepseeks
{
    public class DeepseekResponse
    {
        public string Id { get; set; }
        public string Provider { get; set; }
        public string Model { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public List<Choice> Choices { get; set; }
        public Usage Usage { get; set; }
    }

    public class Choice
    {
        public object Logprobs { get; set; }
        public int Index { get; set; }
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
        public object Refusal { get; set; }
        public object Reasoning { get; set; }
    }

    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }
}
