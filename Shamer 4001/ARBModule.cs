using Discord;
using Discord.Commands;
using Shamer_4001.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shamer_4001
{
    public class ARBModule : ModuleBase<SocketCommandContext>
    {
        [Command("shame")]
        public async Task ShameAsync(string ign, int toPrint = 3, int minGames = 50)
        {
            await Program.Log(new LogMessage(LogSeverity.Info, "shameAsync", "Shame Invoked"));
            await ReplyAsync($"shame {ign} {toPrint} {minGames}");
        }

        [Command("flex")]
        public async Task FlexAsync(string ign, int toPrint = 3, int minGames = 50)
        {
            Console.WriteLine("flex invoked");
            var resp = new FlexShame(0, "r", ign, minGames, toPrint, true, "kd");
            Console.WriteLine("FlexShame obj created");
            resp.BuildRet();
            Console.WriteLine("BuildRet() complete");
            Console.WriteLine($"retList size: {resp.retList.Count}");
            foreach (var v in resp.retList) v.print();
            await ReplyAsync($"flegsed {ign} {toPrint} {minGames}");
        }

        [Command("cherrypick")]
        public async Task CPAsync(string ign, string vehicle)
        {
            var resp = new Cherrypick(0, "r", ign, vehicle);
            resp.BuildRet();
            foreach (var v in resp.retList) v.print();
            Console.WriteLine($"{resp.retList.Count}");
            await ReplyAsync($"churry {ign} {vehicle}");
        }
    }
}
