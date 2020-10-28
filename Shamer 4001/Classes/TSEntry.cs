using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Shamer_4001
{
    //Abstract Class whose implementations will take care of command.
    //Includes webscraping methods and defines instance members that all commands require. 
    //Gets html elements and converts them to an organized Vehicle object.
    //Has a list of vehicles which represents the vehicle/stats that will be sent to discord embed.
    public abstract class TSEntry
    {
        public List<Vehicle> retList;
        public int vtype; // 0: Planes 1: Tanks
        public string mode; // a: arcade r: realistic s: simulator
        public string ign;

        public TSEntry(int v, string md, string un)
        {
            vtype = v;
            mode = md;
            ign = un;
            retList = new List<Vehicle>();
        }

        public HtmlNode[] GetTrArray() //Obtain elements of correct thunderskill table (arcade||RB & Air||Ground)
        {
            var wc = new WebClient();
            
            
            string res = wc.DownloadString($"http://thunderskill.com/en/stat/{ign}/vehicles/{mode}");

            var doc = new HtmlDocument();
            doc.LoadHtml(res);
            var ret = doc.DocumentNode.Descendants("table").ToArray()[vtype].Descendants("tr").ToArray(); 
            if (TrToVehicle(ret[10]) is null) throw new Exception("Name oof"); 
            return ret;


        }


        public static string GetNameFromTr(HtmlNode tr)
        {
            string inp = tr.Elements("td").ToArray()[1].Element("span").InnerText;
            return Regex.Replace(inp, "[^A-Za-z0-9]", "").ToLower();
        }

        public static string GetFullNameFromTr(HtmlNode tr)
        {
            return tr.Elements("td").ToArray()[1].Element("span").InnerText;
        }
        public Vehicle TrToVehicle(HtmlNode tr)
        {
            try { return UnsafeTrToVehicle(tr); }
            catch { return null; }
        }
        public Vehicle UnsafeTrToVehicle(HtmlNode tr) //Take table element (one of e's in array returned by GetTrArray()) use it to create Vehicle obj
        {
            var ret = new Vehicle
            {
                FullName = GetFullNameFromTr(tr),
                name = Regex.Replace(GetNameFromTr(tr), "[^A-Za-z0-9]", ""),
                tier = tr.Element("td").InnerText,
                role = tr.Attributes["data-role"].Value
            };

            HtmlNode[] lis_arr = tr
                        .Elements("td")
                        .ToArray()[1]
                        .Element("div")
                        .Element("ul")
                        .Elements("li")
                        .ToArray();

            HtmlNode[] spans;
            foreach (var li in lis_arr)
            {
                spans = li.Elements("span").ToArray();
                if (spans.Length != 2) continue;
                ret.AddField(spans[0].InnerText, spans[1].InnerText);
            }

            return ret;

        }

        //public abstract bool VComp(Vehicle A, Vehicle B); //compare vehicles for adding to ret list. depends on flex, shame.
        
        public abstract void BuildRet();

        public void PrintList()
        {
            foreach (Vehicle v in retList)
            {
                v.print(); 
                
            }
        }

    }

    public class TestImp : TSEntry
    {
        public TestImp() : base(0, null, null)
        {
            vtype = 0;
            mode = "r";
            ign = "DEFYN";
            retList = new List<Vehicle>();
        }

        override public void BuildRet()
        {
            var trs = GetTrArray();
            for (int i = 1; i<7; i++)
            {
                retList.Add(TrToVehicle(trs[i]));
            }
        }


    }

}
