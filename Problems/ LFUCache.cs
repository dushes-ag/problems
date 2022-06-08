using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/lfu-cache/
public class LFUCacheTest
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] args, int capacity, int?[] expected)
    {
        //act
        var obj = new LFUCache(capacity);
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
        result.Should().BeEquivalentTo(expected);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[][]{new int[]{1,1},new int[]{2,2},new int[]{1},new int[]{3,3},new int[]{2},new int[]{3},new int[]{4,4},
                    new int[]{1},new int[]{3},new int[]{4}},
                2,
                new int?[]{null, null, 1, null, -1, 3, null, -1, 3, 4}},
            new object []{
                new int[][]{new int[]{2},new int[]{2,6},new int[]{1},new int[]{1,5},new int[]{1,2},new int[]{1},new int[]{2}},
                2,
                new int?[]{-1, null, -1, null,  null, 2, 6}}
        };
    }
    public class LFUCache
    {
        private Dictionary<int, Item> _cache = new();
        private Dictionary<int, LinkedListNode<Counter>> _lastNodes = new();
        private LinkedList<Counter> _counters = new();

        private readonly int _capacity;

        public LFUCache(int capacity)
        {
            _capacity = capacity;
        }

        public int Get(int key)
        {
            if (!_cache.ContainsKey(key))
            {
                return -1;
            }

            IncrementCount(key);

            return _cache[key].Value;
        }

        public void Put(int key, int value)
        {
            if (!_cache.ContainsKey(key) && _capacity == _cache.Count)
            {
                if (_capacity == 0)
                {
                    return;
                }
                var toRemove = _counters.First!;
                _cache.Remove(toRemove.Value.Key);
                RemoveNode(toRemove);
            }

            if (!_cache.ContainsKey(key))
            {
                var node = new LinkedListNode<Counter>(new() { Key = key });
                _cache[key] = new Item { Value = value, Node = node };
                _lastNodes[node.Value.Count] = node;
                _counters.AddFirst(node);
            }

            _cache[key].Value = value;
            IncrementCount(key);
        }

        private void IncrementCount(int key)
        {
            var node = _cache[key].Node;
            var addAfter = _lastNodes.ContainsKey(node.Value.Count + 1)
                ? _lastNodes[node.Value.Count + 1]
                : _lastNodes[node.Value.Count];
            var newNode = new LinkedListNode<Counter>(new() { Key = key, Count = node.Value.Count + 1 });
            _cache[key].Node = newNode;
            _counters.AddAfter(addAfter, newNode);
            _lastNodes[newNode.Value.Count] = newNode;
            RemoveNode(node);
        }

        private void RemoveNode(LinkedListNode<Counter> node)
        {
            if (_lastNodes[node.Value.Count] == node)
            {
                if (node.Previous != null && node.Previous.Value.Count == node.Value.Count)
                {
                    _lastNodes[node.Value.Count] = node.Previous;
                }
                else
                {
                    _lastNodes.Remove(node.Value.Count);
                }
            }
            _counters.Remove(node);
        }

        class Counter
        {
            public int Key { get; set; }
            public int Count { get; set; }
        }

        class Item
        {
            public int Value { get; set; }
            public LinkedListNode<Counter> Node { get; set; } = null!;
        }
    }
}