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
        readonly IDictionary<EventTypes, UnityEvent<EventInfo>> events = new Dictionary<EventTypes, UnityEvent<EventInfo>>();

        
        public void AddEvent(EventTypes type,UnityAction<EventInfo> action)
        {
            if(!events.ContainsKey(type))
            {
                events.Add(type, new UnityEvent<EventInfo>());
            }
            events[type].AddListener(action);
        }

        public void RemoveEvent(EventTypes type, UnityAction<EventInfo> action)
        {
            if (events.ContainsKey(type))
            {
                events[type].RemoveListener(action);
            }
        }

        public void ExcuteEvent(EventTypes type,EventInfo info) 
        {
            if (events.ContainsKey(type))
            {
                events[type].Invoke(info);
            }
        }
    }
}