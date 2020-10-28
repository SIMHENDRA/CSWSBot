using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shamer_4001.CommandModules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("info")]
        [Alias("about", "whoami", "owner")]
        public async Task InfoAsync()
        {
            await ReplyAsync("```Written by ssn aka simhendra aka simhendra-sama aka simhendramadhya_ in C# using discord.NET\n" +
                "Scrapes stats from thunderskill\n" +
                "Some users just don't work right, i.e. The_6th_Amry@psn\n" +
                "Use @live or @psn in ign where applicable.\n" +
                "Commands are identical to Shamer 4000, except use & prefix.\n" +
                "Commands: flex, shame, flexkb, shamekb, tflex, tshame, tflexkb, tshamekb (t for ground rb)\n" +
                "Syntax: &command ign optional:min games to show up in result, default 50 \n" +
                "Also, cherrypick/tcherrypick ign vehiclename```"
                );
        }
    }
}
