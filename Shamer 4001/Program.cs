using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Shamer_4001
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _cmds;
        private CommandHandler _cmdh;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;

            _cmds = new CommandService();
            _cmdh = new CommandHandler(_client, _cmds);
            await _cmdh.InstallCommandsAsync();
            
            await _client.LoginAsync(TokenType.Bot, "NzY4ODkxNTI3MjgzNzM2NjM3.X5HECQ.iT69lBuybIJI695UApbWnSQAl90");
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        public static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


    }
}
