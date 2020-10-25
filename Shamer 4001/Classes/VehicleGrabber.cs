using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shamer_4001.Classes
{
    public class VehicleGrabber : TSEntry
    {
        public override void BuildRet()
        {
            throw new NotImplementedException();
        }
        public VehicleGrabber(int v, string md, string un) : base(v, md, un)
        {
            HtmlNode[] trs = GetTrArray();
            Vehicle temp;
            foreach (var tr in trs)
            {
                temp = TrToVehicle(tr);
                if (temp is null) continue;
                temp.print();
            }
            
        }
    }
}
