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

            retList = new List<Vehicle>(num);         
        }

        public bool FlexCompare(Vehicle A, Vehicle B) //A is candidate, B is retList element
        {          
            return (B==null) || (float.Parse(A.fields[met]) > float.Parse(A.fields[met]));
        }

        public bool ShameCompare(Vehicle A, Vehicle B) //A is candidate, B is retList element
        {
            if (B != null) Console.WriteLine($"Comparing {float.Parse(A.fields[met])} to {float.Parse(B.fields[met])}");
            return (B == null) || (float.Parse(A.fields[met]) < float.Parse(A.fields[met]));
        }

        public override void BuildRet()
        {
            var trs = GetTrArray();
            Console.WriteLine($"TrArrayLength : {trs.Length}");

            Vehicle temp;
            foreach (var tr in trs)
            {
                try
                {
                    temp = TrToVehicle(tr);
                    for (int ind = num-1; ind>=0; ind++) //Compare candidate to retList entries
                    {
                        if (compare(temp, retList[ind])) continue;
                        else 
                        {
                            try { retList.Insert(ind + 1, TrToVehicle(tr)); } //caught if candidate didn't pass lowest on retList
                            catch { Console.WriteLine("InsertError"); }
                            break;
                        }
                    }
                    try { retList.RemoveAt(num); Console.WriteLine("RemovedAt"); } //caught if candidate didn't pass lowest entry, else delete whatever was bumped down by candidate
                    catch { Console.WriteLine("RemovedAtError"); }
                }
                catch { continue; }
            }
        }


    }
}
