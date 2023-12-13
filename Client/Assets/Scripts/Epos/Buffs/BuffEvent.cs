using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Epos
{
    public class BuffEvent : MonoBehaviour
    {
        public class EventInfo
        {
            public Item obtainedItem;
            public BonusStat<Define.ActorStatType> stat;
        }
        readonly IDictionary<Define.BuffEventType, UnityEvent<EventInfo>> events = new Dictionary<Define.BuffEventType,UnityEvent<EventInfo>>();

        
        public void AddEvent(Define.BuffEventType type,UnityAction<EventInfo> action)
        {
            if(!events.ContainsKey(type))
            {
                events.Add(type, new UnityEvent<EventInfo>());
            }
            events[type].AddListener(action);
        }

        public void RemoveEvent(Define.BuffEventType type, UnityAction<EventInfo> action)
        {
            if (events.ContainsKey(type))
            {
                events[type].RemoveListener(action);
            }
        }

        public void ExcuteEvent(Define.BuffEventType type,EventInfo info) 
        {
            if (events.ContainsKey(type))
            {
                events[type].Invoke(info);
            }
        }
    }
}