using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Shamer_4001.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Shamer_4001
{
    public class ARBModule : InteractiveBase //ModuleBase<SocketCommandContext>
    {
        [Command("shame", RunMode = RunMode.Async)]
        public async Task ShameAsync(string ign, int toPrint = 10, int minGames = 50)
        {
            var resp = new FlexShame(0, "r", ign, minGames, toPrint, false, "kd");
            //foreach (var v in resp.retList) v.print();
            //await ReplyAsync($"flegsed {ign} {toPrint} {minGames}");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign));
        }

        [Command("flex", RunMode = RunMode.Async)]
        public async Task FlexAsync(string ign, int toPrint = 10, int minGames = 50)
        {           
            var resp = new FlexShame(0, "r", ign, minGames, toPrint, true, "kd");
            //foreach (var v in resp.retList) v.print();
            //await ReplyAsync($"flegsed {ign} {toPrint} {minGames}");
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign));
        }

        [Command("cherrypick", RunMode = RunMode.Async)]
        public async Task CPAsync(string ign, string vehicle)
        {
            var resp = new Cherrypick(0, "r", ign, vehicle);
            //foreach (var v in resp.retList) v.print();
            await NewPagedReplyAsync(VehiclesToEmbeds.VecListToEmbed(resp.retList, resp.vtype, resp.ign));
        }

        [Command("TestGrab", RunMode = RunMode.Async)]
        public async Task TestGrab(string ign)
        {
            var resp = new VehicleGrabber(1, "r", ign);
            resp.retList[10].print();
            await ReplyAsync("Reply Reached");
        }
        [Command("TestPag")]
        public async Task TestPag()
        {

            var Author = new EmbedAuthorBuilder().WithName("ssn's shamer 4001");
            var Author2 = new EmbedAuthorBuilder().WithName("egoioiwe");
            var footer = new EmbedFooterBuilder().WithText("[Thunderskill Page](http://thunderskill.com/en/stat/DEFYN/vehicles/r)");
            var field1 = new EmbedFieldBuilder().WithValue("Battles").WithName("5").WithIsInline(true);
            var field2 = new EmbedFieldBuilder().WithValue("Kills").WithName("900000").WithIsInline(true);
            var field3 = new EmbedFieldBuilder().WithValue("Deaths").WithName("1").WithIsInline(true);
            var field4 = new EmbedFieldBuilder().WithValue("Winrate").WithName("1").WithIsInline(true);
            var field5 = new EmbedFieldBuilder().WithValue("Kills/Death").WithName("15").WithIsInline(true);
            var field6 = new EmbedFieldBuilder().WithValue("Kills/Battle").WithName("1").WithIsInline(true);
            var embed1 = new EmbedBuilder()
                .AddField(field1)
                .AddField(field2)
                .AddField(field3)
                .AddField(field4)
                .AddField(field5)
                .AddField(field6)
                .WithAuthor(Author)
                .WithTitle("Spitfire mk 9")
                .AddField("For more info", "[Thunderskill Page](http://thunderskill.com/en/stat/DEFYN/vehicles/r)")
                .WithFooter("Page 1")
                .Build();
            var embed2 = new EmbedBuilder()
                .AddField(field1)
                .AddField(field2)
                .AddField(field3)
                .AddField(field4)
                .AddField(field5)
                .AddField(field6)
                .WithAuthor(Author)
                .WithTitle("Spitfiegwere mk 9")
                .AddField("For more info", "[Thunderskill Page](http://thunderskill.com/en/stat/simhendramadhya_/vehicles/r)")
                .WithFooter("Page 2")
                .Build();

            //await ReplyAsync(embed: embed1);

            //string repl1 = "```\nSpitfire mk 9\n\tBattles: 10\n\tKills: 6\n\tDeaths: 40\n\tWinrate: 0.34\n```";
            //string repl2 = "```\nSpitfire mk 239\n\tBattles: 1230\n\tKills: 623\n\tDeaths: 240\n\tWinrate: 0.934\n```";
            await NewPagedReplyAsync(new[] { embed1, embed2 });
        }

        public Task<IUserMessage> NewPagedReplyAsync(IEnumerable<object> pages, bool fromSourceUser = true)
        {
            var pager = new PaginatedMessage
            {
                Pages = pages
            };
            return NewPagedReplyAsync(pager, fromSourceUser);
        }
        public Task<IUserMessage> NewPagedReplyAsync(PaginatedMessage pager, bool fromSourceUser = true)
        {
            var criterion = new Criteria<SocketReaction>();
            if (fromSourceUser)
                criterion.AddCriterion(new EnsureReactionFromSourceUserCriterion());
            return NewPagedReplyAsync(pager, criterion);
        }
        public Task<IUserMessage> NewPagedReplyAsync(PaginatedMessage pager, ICriterion<SocketReaction> criterion)
            => Interactive.NewSendPaginatedMessageAsync(Context, pager, criterion);
    }

    public static class InteractiveServiceExtension
    {
        public static async Task<IUserMessage> NewSendPaginatedMessageAsync(this InteractiveService IS,
            SocketCommandContext context,
            PaginatedMessage pager,
            ICriterion<SocketReaction> criterion = null)
        {
            var callback = new CustomPMC(IS, context, pager, criterion);
            await callback.DisplayAsync().ConfigureAwait(false);
            return callback.Message;
        }
    }

    internal class EnsureReactionFromSourceUserCriterion : ICriterion<SocketReaction>
    {
        public Task<bool> JudgeAsync(SocketCommandContext sourceContext, SocketReaction parameter)
        {
            bool ok = parameter.UserId == sourceContext.User.Id;
            return Task.FromResult(ok);
        }
    }
}
