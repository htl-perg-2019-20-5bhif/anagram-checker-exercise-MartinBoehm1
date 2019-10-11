using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anagram
{
    public class Logic
    {

        public bool checkForAnagram(string val1, string val2)
        {
            return SortString(val1).Equals(SortString(val2));
        }
        public string SortString(string input)
        {
            char[] characters = input.ToCharArray();
            Array.Sort(characters);
            return new string(characters);
        }
        public List<string> getAllAnagrams(List<bool> setLetters, List<char> word)
        {
            var i = 0;
            var found = false;
            List<string> anagrams = new List<String>();
            while (i < setLetters.Count())
            {
                if (setLetters[i])
                {
                    bool b = true;
                    char c = word[i];
                    word.Remove(word[i]);
                    setLetters.Remove(setLetters[i]);
                    for (int k = 0; k < word.Count; k++)
                    {
                        if (!setLetters[i])
                        {
                            word.Insert(k, c);
                            setLetters.Insert(k, b);
                            b = setLetters[k + 1];
                            c = word[k + 1];
                            word.Remove(word[k + 1]);
                            setLetters.Remove(setLetters[k + 1]);
                            word.Insert(i, c);
                            setLetters.Insert(i, b);
                            anagrams.AddRange(getAllAnagrams(setLetters, word));
                            found = true;
                        }
                    }
                    break;
                }
                i++;
            }
            if (found)
            {
                return anagrams;
            }
            else
            {
                List<string> l = new List<string>();
                l.Add(new String(word));
                return l;
            }
        }
        public List<string> getAnagrams(string val, List<string> alreadySeen)
        {
            string[] knownAnagrams = System.IO.File.ReadAllText("Dictionary.txt").Replace("\r", string.Empty).Split('\n');
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
