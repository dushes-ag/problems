using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/alien-dictionary/
public class AlienOrder
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string[] words, string expected)
    {
        //act
        var result = new Solution().AlienOrder(words);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new string[] { "wrt","wrf","er","ett","rftt" },
                "wertf"},
            new object[]{
                new string[] { "z","x","z" },
                ""},
            new object[]{
                new string[] { "abc","ab" },
                ""}
        };
    }

    public class Solution
    {
        public string AlienOrder(string[] words)
        {
            var relations = GetRelations(words);
            if (relations == null)
            {
                return string.Empty;
            }
            var (charPredecessors, charSuccessors) = relations.Value;

            var result = new StringBuilder();
            var queue = new Queue<char>();
            foreach (var (item, successors) in charSuccessors)
            {
                if (successors == 0)
                {
                    queue.Enqueue(item);
                }
            }

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                result.Insert(0, item);
                if (!charPredecessors.ContainsKey(item))
                {
                    continue;
                }
                foreach (var pred in charPredecessors[item])
                {
                    charSuccessors[pred]--;
                    if (charSuccessors[pred] == 0)
                    {
                        queue.Enqueue(pred);
                    }
                }
            }

            return charSuccessors.Any(_ => _.Value != 0) ? string.Empty : result.ToString();
        }


        private static (Dictionary<char, HashSet<char>> predecessors, Dictionary<char, int> successors)? GetRelations(string[] words)
        {
            var charPredecessors = new Dictionary<char, HashSet<char>>();
            var charSuccessors = new Dictionary<char, int>();

            foreach (var word in words)
            {
                foreach (var ch in word)
                {
                    charSuccessors[ch] = 0;
                }
            }
            for (var i = 0; i < words.Length - 1; i++)
            {
                for (var k = i + 1; k < words.Length; k++)
                {
                    var prevWord = words[i];
                    var word = words[k];
                    if (prevWord.Length > word.Length && prevWord.StartsWith(word))
                    {
                        return null;
                    }
                    for (var j = 0; j < Math.Min(prevWord.Length, word.Length); j++)
                    {
                        if (prevWord[j] == word[j])
                        {
                            continue;
                        }
                        charPredecessors[word[j]] = charPredecessors.GetValueOrDefault(word[j]) ?? new HashSet<char>();
                        if (!charPredecessors[word[j]].Contains(prevWord[j]))
                        {
                            charPredecessors[word[j]].Add(prevWord[j]);
                            charSuccessors[prevWord[j]]++;
                        }
                        break;
                    }

                }
            }

            return (charPredecessors, charSuccessors);
        }
    }
}