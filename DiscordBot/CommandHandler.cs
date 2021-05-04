﻿using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;

        private CommandService _service;
        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;

            _service = new CommandService();

            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var message = s as SocketUserMessage;
            if (message == null)
                return;

            var context = new SocketCommandContext(_client, message);

            int argPos = 0;
            if (message.HasCharPrefix('!', ref argPos)) ;
            {
                var result = await _service.ExecuteAsync(context, argPos);
            }

        }
    }
}