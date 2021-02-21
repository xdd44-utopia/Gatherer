using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///进入攻击范围条件
    ///</summary>
    public class WithinAttackRangeTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.allTargetTF.Count != 0;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.WithinAttackRange;
        }
    }
}
