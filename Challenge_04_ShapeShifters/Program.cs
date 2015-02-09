using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge_04_ShapeShifters
{
    class Program
    {
        // TODO: http://en.wikipedia.org/wiki/Levenshtein_distance

        static void Main(string[] args)
        {
            var input = args[0].Split('\n').ToList();
            var start = input[0].Replace("\r", "");
            var end = input[1].Replace("\r", "");

            var tempValid = input.Skip(2).ToArray();
            string[] validDna = new string[tempValid.Length];
            for (var y = 0; y < validDna.Length; y++)
            {
                validDna[y] = tempValid[y].Replace("\r", "");
            }

            char[] nucleotides = { 'A', 'C', 'G', 'T' };
            var transitions = new List<string>();
            var current = start;

            while (current != end)
            {
                foreach (var nucleotide in nucleotides) 
                { 
                    for (var j = 0; j < current.Length; j++)
                    { 
                        var temp = start.ToCharArray();
                        temp[j] = nucleotide;
                        current = new string(temp);

                        if (validDna.Contains(current) && !transitions.Any( str => str.Contains(current)))
                        {
                            transitions.Add(current);
                            start = current;
                        }
                    }
                }
                if (start == end) break;
            }

            for(var i=0; i<transitions.Count; i++)
            {
                Console.Write(i == transitions.Count - 1 ? "{0}" : "{0}->", transitions[i]);
            }

            Console.Read();
        }
    }
}