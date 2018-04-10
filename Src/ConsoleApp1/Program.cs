using System;
using System.Collections.Generic;
using JsonDiffPatchDotNet;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var app1 = new Application();
                var app2 = new Application();
                app2.Name = "moi";
                app2.Date = DateTime.Today;
                app2.InnerData.Roles[1] = "Super admin";
                app2.InnerData.Roles.Add("User");
                app2.AnotherInnerData = new InnerData();
                app2.AnotherInnerData.Text = "New inner data set";

                var token1 = JToken.FromObject(app1);
                var token2 = JToken.FromObject(app2);

                var diff = new JsonDiffPatch();
                var res = diff.Diff(token1, token2);

                foreach (var operation in res)
                {
                    var data = JsonConvert.SerializeObject(operation);
                    Console.WriteLine(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }

            Console.ReadLine();
        }
    }

    public class Application
    {
        public Application()
        {
            this.InnerData = new InnerData() { Text = "my inner", Roles = new List<string>() { "Admin", "Default" } };
            this.Name = "Test";
            this.Age = 20;
            this.Date = DateTime.Now;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public InnerData InnerData { get; set; }
        public InnerData AnotherInnerData { get; set; }
    }

    public class InnerData
    {
        public string Text { get; set; }
        public List<string> Roles { get; set; }
    }
}
