using Anthropic;
using Anthropic.Models.Messages;
using Anthropic.Core;

namespace DotNetxAnthropic
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("pass your API key while running app: dotnet run -- <ANTHROPIC_API_KEY>");
                return;
            }

            ClientOptions clientOptions = new()
            {
                ApiKey = args[0]
            };

            AnthropicClient client = new(clientOptions);

            MessageCreateParams request = new()
            {
                Model = Model.ClaudeOpus4_7,
                MaxTokens = 100,
                Messages =
                [
                    new MessageParam
                    {
                        Role = Role.User,
                        Content = "Write a code in c# to say - Peace!"
                    }
                ]
            };

            var response = await client.Messages.Create(request);

            if (response.Content.Count > 0
                && response.Content[0].TryPickText(out TextBlock? textBlock)
                && textBlock is not null)
            {
                Console.WriteLine(textBlock.Text);
                return;
            }

            Console.Error.WriteLine("Claude returned no text content.");
        }
    }
}

