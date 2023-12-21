using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class CommonMonster : Monster
    {
        protected override IEnumerator UseTurn()
        {
            yield return null;
        }
    }
}