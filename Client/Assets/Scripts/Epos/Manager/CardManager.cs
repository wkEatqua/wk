using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Epos
{
    public class CardManager
    {
        static public readonly IDictionary<Define.CardPattern,List<long>> BuffList = new Dictionary<Define.CardPattern,List<long>>();
        public readonly static IDictionary<Define.CardPattern, int> CardPatternCounts = new Dictionary<Define.CardPattern, int>();
        static CardManager() 
        {
            BuffList.Clear();   
            foreach(Define.CardPattern pattern in Enum.GetValues(typeof(Define.CardPattern)))
            {
                BuffList.Add(pattern, new List<long>());
            }
            CardPatternCounts.Clear();

            foreach (Define.CardPattern pattern in Enum.GetValues(typeof(Define.CardPattern)))
            {
                CardPatternCounts.Add(pattern, 0);
            }
        }
    }
}