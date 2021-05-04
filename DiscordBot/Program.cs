using System;
using Discord.WebSocket;
using Discord;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Program
    {
        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        //https://youtu.be/egq26JwyJkc?t=953

        private DiscordSocketClient _client;
        private CommandHandler _handler;

        string token = "";
        public async Task StartAsync()
        {
        
            _client = new DiscordSocketClient();

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            _handler = new CommandHandler(_client);

            //Ko je bot pripravljen nam v konzolo napiše
            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected - (" + DateTime.Now + ")");
                Console.WriteLine("---------------------------------------------");
                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }
    }
}
