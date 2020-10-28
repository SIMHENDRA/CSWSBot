using System;
using System.Collections;
using System.Collections.Generic;
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
