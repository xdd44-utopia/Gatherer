using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///目标被击杀条件
    ///</summary>
    public class TargetKilledTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.targetTF.GetComponentInChildren<HealthbarController>().hp <= 0;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.TargetKilled;
        }
    }
}
