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

        //https://docs.stillu.cc/guides/introduction/intro.html

        /*      TODO
         *     -Play music
         *     -More commands (INFO, MC start) +
         *     -Output: Use EmbedBuilder + StringBuilder 
         */

        private DiscordSocketClient _client;
        private CommandHandler _handler;

        string token = "";
        public async Task StartAsync()
        {
        
            _client = new DiscordSocketClient();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _handler = new CommandHandler(_client);

            //Ko je BOT pripravljen
            _client.Ready += () =>
            {
                StartUP();

                return Task.CompletedTask;
            };
            
            await Task.Delay(-1);   
        }


        //Metoda, ki nastavi nastavitve konzole in izpiše da je BOT online
        static void StartUP()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("Bot is connected - (" + DateTime.Now + ")");
            Console.WriteLine("---------------------------------------------");
        }
    }
}
