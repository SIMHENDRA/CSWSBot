using Discord.WebSocket;
using Shamer_4001.Classes;
using System;
using Discord.Commands;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shamer_4001
{
    public class GroundModule : ShamerModuleBase
    {
        [Command("tshame", RunMode = RunMode.Async)]
        public async Task tShameAsync(string ign, int minGames = 50, int toPrint = 10)
        {
            var resp = new FlexShame(1, "r", ign, minGames, toPrint, false, "kd");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }

        [Command("tflex", RunMode = RunMode.Async)]
        public async Task tFlexAsync(string ign, int minGames = 50, int toPrint = 10)
        {
            var resp = new FlexShame(1, "r", ign, minGames, toPrint, true, "kd");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }

        [Command("tshamekb", RunMode = RunMode.Async)]
        public async Task tShamekbAsync(string ign, int minGames = 50, int toPrint = 10)
        {
            var resp = new FlexShame(1, "r", ign, minGames, toPrint, false, "kb");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }

        [Command("tflexkb", RunMode = RunMode.Async)]
        public async Task tFlexkbAsync(string ign, int minGames = 50, int toPrint = 10)
        {
            var resp = new FlexShame(1, "r", ign, minGames, toPrint, true, "kb");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }

        [Command("tcherrypick", RunMode = RunMode.Async)]
        public async Task CPAsync(string ign, string vehicle)
        {
            var resp = new Cherrypick(1, "r", ign, vehicle);
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }
    }
}
