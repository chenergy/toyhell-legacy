using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class A_PlayerIdleEnter:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Debug.Log("idling");
        }
    }
}