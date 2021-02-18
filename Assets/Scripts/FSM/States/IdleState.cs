using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///闲置状态
    ///</summary>
    public class IdleState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Idle;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.anim.SetBool(fsm.status.chPrams.idle, true);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(fsm.status.chPrams.idle, false);
        }
    }
}
