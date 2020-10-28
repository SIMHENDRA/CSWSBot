using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Shamer_4001.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shamer_4001
{
    public class ShamerModuleBase : InteractiveBase //ModuleBase<SocketCommandContext>
    {
        

        [Command("TestGrab", RunMode = RunMode.Async)]
        public async Task TestGrab(string ign)
        {
            var resp = new VehicleGrabber(1, "r", ign);
            resp.retList[10].print();
            await ReplyAsync("Reply Reached");
        }

        public Task<IUserMessage> NewPagedReplyAsync(IEnumerable<object> pages, string ign, bool fromSourceUser = true)
        {
            if (pages.Count() == 0) throw new Exception("Query returned no vehicle hits");
            var pager = new PaginatedMessage
            {
                Pages = pages,
                Content = $"`{pages.Count()} reasons why {ign} is a shitter.`"
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

    public class EnsureReactionFromSourceUserCriterion : ICriterion<SocketReaction>
    {
        public Task<bool> JudgeAsync(SocketCommandContext sourceContext, SocketReaction parameter)
        {
            bool ok = parameter.UserId == sourceContext.User.Id;
            return Task.FromResult(ok);
        }
    }
}
