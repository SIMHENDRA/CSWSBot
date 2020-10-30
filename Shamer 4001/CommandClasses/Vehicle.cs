using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace Shamer_4001
{
    public class Vehicle
    {
        public string FullName;
        public string name;

        public string tier;
        public string role; 
        public Dictionary<string, string> fields;

        public Vehicle()
        {
            fields = new Dictionary<string, string>();
        }

        public void AddField(string key, string val)
        {
            fields.Add(key, val);

            if (fields.ContainsKey("Winrate")) return;
            else if (!(fields.ContainsKey("Battles") && fields.ContainsKey("Victories"))) return;
            else if (int.Parse(fields["Battles"]) == 0) return;

            float wr =(float.Parse(fields["Victories"]) / (float.Parse(fields["Battles"])));
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.PercentDecimalDigits = 1;
            fields.Add("Winrate", wr.ToString("P", nfi));
            fields.Add("WinrateFloat", wr.ToString());
            return;           
        }

        public void print()
        {
            Console.WriteLine(FullName);
            Console.WriteLine(role);
            Console.WriteLine(tier);
            foreach (var entry in fields)
            {
                Console.WriteLine($"\t{entry.Key} : {entry.Value}");
            }
            Console.WriteLine("--------------");
        }

    }
}
