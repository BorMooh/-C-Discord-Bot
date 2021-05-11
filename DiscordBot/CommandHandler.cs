using Discord.WebSocket;
using Discord;
using Discord.Commands;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot
{
    public class CommandHandler
    {
        //Klient, Commandservice in ServiceProvider
        private DiscordSocketClient _client;
        private CommandService _service;
        private IServiceProvider _serProvider;


        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();


            _service.AddModulesAsync(Assembly.GetEntryAssembly(), _serProvider);
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var message = s as SocketUserMessage;

            //Če je sporočilo null ne naredi nič
            if (message == null)
                return;

            var context = new SocketCommandContext(_client, message);
            int argPos = 0;


            //Če ima sporočilo prefix znak ! 
            if (message.HasCharPrefix('!', ref argPos)) 
            {
                //Vsako komando nam shrani v konzolo, kjer jo tudi lahko vidimo potem
                Console.WriteLine(context.User + "(" + DateTime.Now + ") - " +  message);
                var result = await _service.ExecuteAsync(context, argPos, _serProvider);

                //Javi napako 
                if(!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }

            }

        }
    }
}
