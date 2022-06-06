using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/lru-cache/
public class LRUCacheTest
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] args, int capacity, int?[] expected)
    {
        //act
        var obj = new LRUCache(capacity);
        var result = new List<int?>();
        foreach (var arg in args)
        {
            if (arg.Length == 1)
            {
                result.Add(obj.Get(arg[0]));
            }
            else
            {
                obj.Put(arg[0], arg[1]);
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
                new int[][]{new int[]{1,1},new int[]{2,2},new int[]{1},new int[]{3,3},new int[]{2},new int[]{4,4},new int[]{1},new int[]{3},new int[]{4}},
                2,
                new int?[]{null, null, 1, null, -1, null, -1, 3, 4}}
        };
    }

    public class LRUCache
    {
        private readonly int _capacity;
        private Dictionary<int, (int value, LinkedListNode<int> keyNode)> _cache = new();
        private LinkedList<int> _indexes = new();

        public LRUCache(int capacity)
        {
            _capacity = capacity;
        }

        public int Get(int key)
        {
            if (!_cache.ContainsKey(key))
            {
                return -1;
            }

            Put(key, _cache[key].value);

            return _cache[key].value;
        }

        public void Put(int key, int value)
        {
            if (!_cache.ContainsKey(key) && _indexes.Count == _capacity)
            {
                _cache.Remove(_indexes.First());
                _indexes.RemoveFirst();
            }
            else if (_cache.ContainsKey(key))
            {
                _indexes.Remove(_cache[key].keyNode);
            }
            var node = new LinkedListNode<int>(key);
            _cache[key] = (value, node);
            _indexes.AddLast(node);
        }
    }
}