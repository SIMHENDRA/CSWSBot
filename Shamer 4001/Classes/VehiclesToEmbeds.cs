using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shamer_4001.Classes
{
    public class VehiclesToEmbeds
    {
        public static Embed[] VecListToEmbed(List<Vehicle> retList, int vtype, string ign)
        {
            Embed[] ret = new Embed[retList.Count];

            if (vtype == 0)
            {
                for (int i = 0; i<retList.Count; i++)
                {
                    ret[i] = AirBuilder(retList[i])
                        .WithFooter($"Page {i}  BOT BY SSN!")
                        .WithAuthor(ign)
                        .AddField("For more info", $"[Thunderskill Page](http://thunderskill.com/en/stat/{ign}/vehicles/r)")
                        .Build();
                }
            }
            else
            {
                for (int i = 0; i < retList.Count; i++)
                {
                    ret[i] = GroundBuilder(retList[i])
                        .WithFooter($"Page {i}  BOT BY SSN!")
                        .WithAuthor(ign)
                        .AddField("For more info", $"[Thunderskill Page](http://thunderskill.com/en/stat/{ign}/vehicles/r)")
                        .Build();
                }
            }


            return ret;
        }


        public static EmbedBuilder AirBuilder(Vehicle v)
        {
            var Battles = new EmbedFieldBuilder().WithValue("Battles").WithName(v.fields["Battles"]).WithIsInline(true);
            var Kills = new EmbedFieldBuilder().WithValue("Kills").WithName(v.fields["Overall air frags"]).WithIsInline(true);
            var Deaths = new EmbedFieldBuilder().WithValue("Deaths").WithName(v.fields["Deaths"]).WithIsInline(true);
            var Winrate = new EmbedFieldBuilder().WithValue("Winrate")
                .WithName((float.Parse(v.fields["Victories"])/(float.Parse(v.fields["Battles"]))).ToString())
                .WithIsInline(true);
            var KD = new EmbedFieldBuilder().WithValue("Kills/Death").WithName(v.fields["Air frags / death"]).WithIsInline(true);
            var KB = new EmbedFieldBuilder().WithValue("Kills/Battle").WithName(v.fields["Air frags / battle"]).WithIsInline(true);
            return new EmbedBuilder()
                .AddField(Battles)
                .AddField(Kills)
                .AddField(Deaths)
                .AddField(Winrate)
                .AddField(KD)
                .AddField(KB)
                .WithTitle(v.FullName);
        }


        public static EmbedBuilder GroundBuilder(Vehicle v)
        {
            var Battles = new EmbedFieldBuilder().WithValue("Battles").WithName(v.fields["Battles"]).WithIsInline(true);
            var Kills = new EmbedFieldBuilder().WithValue("Kills").WithName(v.fields["Overall ground frags"]).WithIsInline(true);
            var Deaths = new EmbedFieldBuilder().WithValue("Deaths").WithName(v.fields["Deaths"]).WithIsInline(true);
            var Winrate = new EmbedFieldBuilder().WithValue("Winrate")
                .WithName((float.Parse(v.fields["Victories"]) / (float.Parse(v.fields["Battles"]))).ToString())
                .WithIsInline(true);
            var KD = new EmbedFieldBuilder().WithValue("Kills/Death").WithName(v.fields["Ground frags / death"]).WithIsInline(true);
            var KB = new EmbedFieldBuilder().WithValue("Kills/Battle").WithName(v.fields["Ground frags / battle"]).WithIsInline(true);
            return new EmbedBuilder()
                .AddField(Battles)
                .AddField(Kills)
                .AddField(Deaths)
                .AddField(Winrate)
                .AddField(KD)
                .AddField(KB)
                .WithTitle(v.FullName);
        }



    }
}
