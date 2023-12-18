using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Shared.Model;

namespace Epos
{
    public class BuffEvent : MonoBehaviour
    {
        public class EventInfo
        {
            public Item obtainedItem;
            public BonusStat<ActorStatType> stat;
        }
        readonly IDictionary<BuffEventType, UnityEvent<EventInfo>> events = new Dictionary<BuffEventType,UnityEvent<EventInfo>>();

        
        public void AddEvent(BuffEventType type,UnityAction<EventInfo> action)
        {
            if(!events.ContainsKey(type))
            {
                events.Add(type, new UnityEvent<EventInfo>());
            }
            events[type].AddListener(action);
        }

        public void RemoveEvent(BuffEventType type, UnityAction<EventInfo> action)
        {
            if (events.ContainsKey(type))
            {
                events[type].RemoveListener(action);
            }
        }

        public void ExcuteEvent(BuffEventType type,EventInfo info) 
        {
            if (events.ContainsKey(type))
            {
                events[type].Invoke(info);
            }
        }
    }
}