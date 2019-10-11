using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anagram
{
    public class Logic
    {

        public bool checkForAnagram(string val1, string val2) { 
            return SortString(val1).Equals(SortString(val2));
        }
        public string SortString(string input)
        {
            char[] characters = input.ToCharArray();
            Array.Sort(characters);
            return new string(characters);
        }
        public List<List<char>> getAllAnagrams(List<bool> setLetters, List<List<char>> word)
        {
            var found = false;
            List<string> anagrams = new List<string>();
            var i = 0;
            while (i < setLetters.Count())
            {
                if (!setLetters[i])
                {
                    var lastIndex = i;
                    for (int k = 0; k < word.Count; k++)
                    {
                        if (!setLetters[i])
                        {
                            setLetters[k] = true;
                            List<List<char>> l = new List<List<char>>();
                            l.Add(exchangeChar(i, k, word[0]));
                            var former = getAllAnagrams(setLetters, l).ToArray();
                            for (int j = 0; j < former.Length; j++)
                            {
                                anagrams.Add(new string(former[j].ToArray()));
                            }
                            found = true;
                            lastIndex = k;
                        }
                    }
                    break;
                }
                i++;
            }
            if (found)
            {
                var toReturn = new List<List<char>>();
                for (int j = 0; j < anagrams.Count; j++)
                {
                    toReturn.Add(new List<char>(anagrams[j].ToCharArray()));
                }
                return toReturn;
            }
            else
            {
                List < List<char> > l = new List<List<char>>();
                l.AddRange(word);
                return l;
            }
        }
        public List<char> exchangeChar(int index1, int index2, List<char> list)
        {
            List<char> l = new List<char>();
            for(int i = 0; i < list.Count; i++)
            {
                if (i == index1)
                {
                    l.Add(list[index2]);
                }else if (i == index2)
                {
                    l.Add(list[index1]);
                }
                else
                {
                    l.Add(list[i]);
                }
            }
            return l;
        }
        public List<bool> exchangeBool(int index1, int index2, List<bool> list)
        {
            List<bool> l = new List<bool>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == index1)
                {
                    l.Add(list[index2]);
                }
                else if (i == index2)
                {
                    l.Add(list[index1]);
                }
                else
                {
                    l.Add(list[i]);
                }
            }
            return l;
        }
        public List<string> getAnagrams(string val, List<string> alreadySeen)
        {
            string[] knownAnagrams = System.IO.File.ReadAllText("Dictionary.txt").Replace("\r", string.Empty).Split("\n");
            foreach (string thisAna in knownAnagrams)
            {
                string[] theseWords = thisAna.Split(" = ");
                if (theseWords[0].Equals(val))
                {
                    bool seen = false;
                    foreach (string thisSeen in alreadySeen)
                    {
                        if (theseWords[1].Equals(thisSeen))
                        {
                            seen = true;
                            //Console.WriteLine("seen");
                        }
                    }
                    if (!seen)
                    {
                        alreadySeen.Add(theseWords[1]);
                        alreadySeen = getAnagrams(theseWords[1], alreadySeen);
                    }
                }
                else if (theseWords[1].Equals(val))
                {
                    bool seen = false;

                    foreach (string thisSeen in alreadySeen)
                    {
                        if (theseWords[0].Equals(thisSeen))
                        {
                            seen = true;
                        }
                    }
                    if (!seen)
                    {
                        alreadySeen.Add(theseWords[0]);

                        alreadySeen = getAnagrams(theseWords[0], alreadySeen);
                    }
                }
            }
            return alreadySeen;
        }
    }
}
