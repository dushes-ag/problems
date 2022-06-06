using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/time-based-key-value-store/
public class LTimeMapTest
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(object[][] args, string?[] expected)
    {
        //act
        var obj = new TimeMap();
        var result = new List<string?>();
        foreach (var arg in args)
        {
            if (arg.Length == 2)
            {
                result.Add(obj.Get((string)arg[0], (int)arg[1]));
            }
            else
            {
                obj.Set((string)arg[0], (string)arg[1], (int)arg[2]);
                result.Add(null);
            }
        }

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new object[][]{new object[]{"foo", "bar", 1},
                    new object[]{"foo", 1}, new object[]{"foo", 3}, new object[]{"foo", "bar2", 4}, new object[]{"foo", 4}, new object[]{"foo", 5}},
                new string?[]{null, "bar", "bar", null, "bar2", "bar2"}},
            new object []{
                new object[][]{new object[]{"love", "high", 10}, new object[]{"love", "low", 20},
                    new object[]{"love", 5}, new object[]{"love", 10}, new object[]{"love", 15}, new object[]{"love", 20}, new object[]{"love", 25}},
                new string?[]{null, null, "", "high", "high", "low", "low"}},
            new object []{
                new object[][]{new object[]{"a", "bar", 1}, new object[]{"x", "b", 3},
                    new object[]{"b", 3}, new object[]{"foo", "bar2", 4}, new object[]{"foo", 4}, new object[]{"foo", 5}},
                new string?[]{null, null, "", null, "bar2", "bar2"}}
        };
    }

    public class TimeMap
    {
        private readonly Dictionary<string, List<(int timestamp, string value)>> _cache = new();

        public TimeMap()
        {
        }

        public void Set(string key, string value, int timestamp)
        {
            var values = _cache.GetValueOrDefault(key) ?? new List<(int timestamp, string value)>();
            _cache[key] = values;
            values.Add((timestamp, value));
        }

        public string Get(string key, int timestamp)
        {
            var values = _cache.GetValueOrDefault(key);
            if (values == null)
            {
                return string.Empty;
            }

            var from = 0;
            var to = values.Count - 1;
            if (values[0].timestamp > timestamp)
            {
                return string.Empty;
            }
            if (values[to].timestamp <= timestamp)
            {
                return values[to].value;
            }

            while (from <= to)
            {
                var index = (from + to) / 2;
                if (values[index].timestamp <= timestamp && timestamp < values[index + 1].timestamp)
                {
                    return values[index].value;
                }

                if (values[index + 1].timestamp < timestamp)
                {
                    from = index + 1;
                }
                else
                {
                    to = index;
                }
            }
            throw new Exception();
        }
    }
}