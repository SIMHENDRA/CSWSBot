using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shamer_4001.Classes
{
    public class FlexShame : TSEntry
    {

        public int mg; //min games
        public int num; // num vs to print (retList size)
        public string met; //Airfrags/battle, groundfrags/battle, airfrags/game, ... etc. Any numeric stat in Vehicle dictionary to compare by.
        public bool FS; //True if Flex, false if Shame

        public delegate bool Comparor(Vehicle A, Vehicle B);
        public Comparor compare;


        public FlexShame(int v, string md, string un, int minGames, int numVsToPrint, bool fs, string metric) : base(v, md, un)
        {
            mg = minGames;
            num = numVsToPrint;
            if (metric == "kd")
            {
                if (v == 0) met = "Air frags / death";
                else met = "Ground frags / death";

            }
            else
            {
                if (v == 0) met = "Air frags / battle";
                else met = "Ground frags / battle";
            }

            FS = fs;
            if (FS) compare = FlexCompare;
            else compare = ShameCompare;

            retList = new List<Vehicle>();
            for (int i = 0; i<num; i++)
            {
                retList.Add(null);
            }
        }

        public bool FlexCompare(Vehicle A, Vehicle B) //A is candidate, B is retList element
        {
            if (B != null)
            {
                Console.WriteLine($"A: {A.FullName} B: {B.FullName}");
                Console.WriteLine($"Comparing {float.Parse(A.fields[met])} to {float.Parse(B.fields[met])}");
            }
            bool ret = (B==null) || (float.Parse(A.fields[met]) > float.Parse(B.fields[met]));
            Console.WriteLine($"Returning {ret}");
            return ret;
        }

        public bool ShameCompare(Vehicle A, Vehicle B) //A is candidate, B is retList element
        {
            //if (B != null) Console.WriteLine($"Comparing {float.Parse(A.fields[met])} to {float.Parse(B.fields[met])}");
            return (B == null) || (float.Parse(A.fields[met]) < float.Parse(B.fields[met]));
        }

        public override void BuildRet()
        {
            HtmlNode[] trs;
            try { trs = GetTrArray(); }
            catch { throw new Exception("Bad Name."); }
            //Console.WriteLine($"TrArrayLength : {trs.Length}");
            //Console.WriteLine($"initial retListLength : {retList.Count}");

            Vehicle temp;
            foreach (var tr in trs)
            {
                temp = TrToVehicle(tr);
                if (temp is null) continue;
                if (float.Parse(temp.fields["Battles"]) < mg) continue;
                if (!temp.fields.ContainsKey(met)) continue;

                for (int ind = num-1; ind>=0; ind--) //Compare candidate to retList entries
                {
                    if (compare(temp, retList[ind]) && ind == 0)
                    {
                        retList.Insert(0, TrToVehicle(tr));
                        break;
                    }
                    else if (compare(temp, retList[ind])) continue;
                    else
                    {
                        if (ind+1 < num) retList.Insert(ind + 1, TrToVehicle(tr)); //caught if candidate didn't pass lowest on retList
                        break;
                    }
                }
                if (retList.Count > num) retList.RemoveAt(num);                               
            }
        }


    }
}
