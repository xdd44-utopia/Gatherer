using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///all超出攻击范围条件
    ///</summary>
    public class AllOutOfAttackRangeTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.allTargetTF.Count == 0;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.AllOutOfAttackRange;
        }
    }
}
