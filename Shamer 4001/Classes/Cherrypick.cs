using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Shamer_4001.Classes
{
    public class Cherrypick : TSEntry
    {

        public string search;

        public Cherrypick(int v, string md, string un, string inp) : base(v, md, un)
        {
            search = Regex.Replace(inp, "[^A-Za-z0-9]", "").ToLower();
        }

        public override void BuildRet()
        {
            Console.WriteLine($"Search String: {search}");
            var trs = GetTrArray();
            foreach (var tr in trs)
            {
                try { if (GetNameFromTr(tr).Contains(search)) retList.Add(TrToVehicle(tr)); }
                catch { continue; }
            }
        }

    }
}
