using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shamer_4001
{
    [Group("cmd")]
    public class NewModule : ModuleBase<SocketCommandContext>
    {
        [Command("do")]
        public async Task DoAsync(string echo, int inp = 0)
        {
            string yee = $"{echo} {inp} yee";
            await ReplyAsync(yee);
        }
    }
}
