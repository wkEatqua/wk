using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Shared.Model;

namespace Epos
{
    public class CardManager
    {
        static public readonly IDictionary<CardPattern,List<long>> BuffList = new Dictionary<CardPattern,List<long>>();
        public readonly static IDictionary<CardPattern, int> CardPatternCounts = new Dictionary<CardPattern, int>();
        static CardManager() 
        {
            BuffList.Clear();   
            foreach(CardPattern pattern in Enum.GetValues(typeof(CardPattern)))
            {
                BuffList.Add(pattern, new List<long>());
            }
            CardPatternCounts.Clear();

            foreach (CardPattern pattern in Enum.GetValues(typeof(CardPattern)))
            {
                CardPatternCounts.Add(pattern, 0);
            }
        }
    }
}