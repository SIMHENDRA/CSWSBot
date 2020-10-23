using Discord;
using Discord.Commands;
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
            await ReplyAsync($"flex {ign} {toPrint} {minGames}");
        }

        [Command("cherrypick")]
        public async Task CPAsync(string ign, string vehicle)
        {
            await ReplyAsync($"cherrypick {ign} {vehicle}");
        }
    }
}
