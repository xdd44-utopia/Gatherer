using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class WithinMinDistTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            foreach (var item in fsm.allTargetTF)
            {
                return Vector3.Distance(item.Value.position, fsm.transform.position) < fsm.minDistance;
            }
            return false;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.WithinMinDist;
        }

        
    }
}
