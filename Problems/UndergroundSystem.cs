using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/design-underground-system/
public class UndergroundSystemTests
{
    [Fact]
    public void Test()
    {
        //act
        var obj = new UndergroundSystem();
        obj.CheckIn(1, "station1", 3);
        obj.CheckOut(1, "station2", 5);
        obj.CheckIn(2, "station1", 4);
        obj.CheckOut(2, "station2", 5);
        double result = obj.GetAverageTime("station1", "station2");

        //assert
        Assert.Equal(1.5, result);
    }

    public class UndergroundSystem
    {
        private Dictionary<(string from, string to), (double totalDuration, int tripsCount)> _totals = new();
        private Dictionary<int, (string stationName, int time)> _checkIns = new();

        public UndergroundSystem()
        {

        }

        public void CheckIn(int id, string stationName, int t)
        {
            _checkIns.Add(id, (stationName, t));
        }

        public void CheckOut(int id, string stationName, int t)
        {
            var duration = t - _checkIns[id].time;
            var startStation = _checkIns[id].stationName;
            _checkIns.Remove(id);

            var key = (startStation, stationName);
            var totals = _totals.ContainsKey(key) ? _totals[key] : (0, 0);
            _totals[key] = (totals.totalDuration + duration, totals.tripsCount + 1);
        }

        public double GetAverageTime(string startStation, string endStation)
        {
            var totals = _totals[(startStation, endStation)];
            return totals.totalDuration / totals.tripsCount;
        }
    }
}