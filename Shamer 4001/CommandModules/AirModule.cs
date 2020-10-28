using Discord.WebSocket;
using Shamer_4001.Classes;
using System;
using Discord.Commands;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shamer_4001
{
    public class AirModule : ShamerModuleBase
    {
        [Command("shame", RunMode = RunMode.Async)]
        public async Task ShameAsync(string ign, int minGames = 50, int toPrint = 10)
        {
            var resp = new FlexShame(0, "r", ign, minGames, toPrint, false, "kd");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }

        [Command("flex", RunMode = RunMode.Async)]
        public async Task FlexAsync(string ign, int minGames = 50, int toPrint = 10)
        {
            var resp = new FlexShame(0, "r", ign, minGames, toPrint, true, "kd");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }

        [Command("shamekb", RunMode = RunMode.Async)]
        public async Task ShamekbAsync(string ign, int minGames = 50, int toPrint = 10)
        {
            var resp = new FlexShame(0, "r", ign, minGames, toPrint, false, "kb");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }

        [Command("flexkb", RunMode = RunMode.Async)]
        public async Task FlexkbAsync(string ign, int minGames = 50, int toPrint = 10)
        {
            var resp = new FlexShame(0, "r", ign, minGames, toPrint, true, "kb");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }

        [Command("cherrypick", RunMode = RunMode.Async)]
        public async Task CPAsync(string ign, string vehicle)
        {
            var resp = new Cherrypick(0, "r", ign, vehicle);
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign), resp.ign);
        }
    }
}
