﻿using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;

namespace Shamer_4001
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;

            var services = new ServiceCollection()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<InteractiveService>()
                .AddSingleton(_client)
                .BuildServiceProvider();

            await services.GetRequiredService<CommandHandler>().InstallCommandsAsync(services);            
            await _client.LoginAsync(TokenType.Bot, "NzQ3ODkyNjYzNDUzMzUxOTQ3.X0VfUg.NbDf5lcL1tQN6SbLOoHTyDxr0OI");
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        public static Task Log(LogMessage msg)
        {
            Console.WriteLine($"Log in program.cs : {msg.ToString()}");
            return Task.CompletedTask;
        }


    }
}
