using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public interface ICommand
    {
        public IEnumerator Excute();
    }
}