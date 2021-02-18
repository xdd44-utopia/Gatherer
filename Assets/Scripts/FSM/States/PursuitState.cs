using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///追逐状态
    ///</summary>
    public class PursuitState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Pursuit;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.anim.SetBool(fsm.status.chPrams.move, true);
        }

        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            Debug.DrawLine(fsm.transform.position, fsm.targetTF.position);
            //fsm.MoveToTarget(fsm.targetTF.position, fsm.status.attackDistance - 0.1f, fsm.runSpeed);
            fsm.MoveToTarget(fsm.targetTF, fsm.status.attackDistance - 0.1f, fsm.runSpeed);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            //停止移动
            fsm.StopMove();
            fsm.anim.SetBool(fsm.status.chPrams.move, false);
        }
    }
}
