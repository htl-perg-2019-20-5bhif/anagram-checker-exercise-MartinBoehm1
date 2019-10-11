using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace anagram
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logic s = new Logic();
            string val;
            Console.Write("Do you want to start a console-app(1) or a server(2).: ");
            val = Console.ReadLine();
            if (val.Equals("1"))
            {
                Console.Write("Do you want to check if two words are anagrams(1), whether there are known anagrams to a word(2) or what all possible anagrams are(3).: ");
                val = Console.ReadLine();
                if (val.Equals("1"))
                {
                    Console.Write("Enter Words: ");
                    var values = Console.ReadLine().Split(" ");
                    Console.WriteLine(s.checkForAnagram(values[0], values[1]));

                }
                else if (val.Equals("2"))
                {
                    Console.Write("Enter Word: ");
                    val = Console.ReadLine();
                    bool first = true;
                    foreach (string str in s.getAnagrams(val, new string[] { val }.ToList()))
                    {
                        if (!first)
                        {
                            Console.WriteLine(str);
                        }
                        else
                        {
                            first = false;
                        }
                    }
                }else if (val.Equals("3"))
                {
                    Console.Write("Enter Word: ");
                    var value = Console.ReadLine().ToCharArray().ToList();
                    var values = new List<List<char>>();
                    values.Add(value);
                    bool first = true;
                    List<bool> list = new List<bool>();
                    for(int i = 0; i < value.Count; i++)
                    {
                        list.Add(false);
                    }
                    foreach (List<char> str in s.getAllAnagrams(list, values))
                    {
                        if (!first)
                        {
                            foreach(char c in str)
                            {
                                Console.Write(c);
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            first = false;
                        }
                    }
                }
            }
            else if (val.Equals("2"))
            {
                CreateHostBuilder(args).Build().Run();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
