using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.NetworkInformation;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;

namespace Shamer_4001
{
    class Program
    {
        bool prefixReminder = true;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += PrefixReminder;

            var services = new ServiceCollection()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<InteractiveService>()
                .AddSingleton(_client)
                .BuildServiceProvider();

            await services.GetRequiredService<CommandHandler>().InstallCommandsAsync(services);            
            await _client.LoginAsync(TokenType.Bot, "");
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        public static Task Log(LogMessage msg)
        {
            Console.WriteLine($"Log in program.cs : {msg.ToString()}");
            return Task.CompletedTask;
        }

        public async Task PrefixReminder(SocketMessage messageParam)
        {

            if (!prefixReminder) return;

            SocketUserMessage message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            /*if (message.HasStringPrefix("&toggle", ref argPos)) 
            { 
                prefixReminder = !prefixReminder; 
                return; 
            }*/

            if ((message.HasStringPrefix("flex", ref argPos) ||
                message.HasStringPrefix("shame", ref argPos) ||
                message.HasStringPrefix("cherrypick", ref argPos)) &&
                !message.HasMentionPrefix(_client.CurrentUser, ref argPos) &&
                !message.Author.IsBot)
            {
                var context = new SocketCommandContext(_client, message);
                await context.Channel.SendMessageAsync("New shamer, use & before command");
            }
            else return;
            

        }


    }
}
