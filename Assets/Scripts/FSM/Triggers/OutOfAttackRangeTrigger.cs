using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///超出攻击范围条件
    ///</summary>
    public class OutOfAttackRangeTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (fsm.targetTF == null) return false;
            return Vector3.Distance(fsm.targetTF.position, fsm.transform.position) > fsm.status.attackDistance;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.OutOfAttackRange;
        }
    }
}
