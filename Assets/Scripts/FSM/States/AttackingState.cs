using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///攻击状态
    ///</summary>
    public class AttackingState : FSMState
    {
        private FSMBase fSMBase;

        public override void Init()
        {
            StateID = FSMStateID.Attacking;
        }

        private float attackTime;
        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            Debug.DrawLine(fsm.transform.position, fsm.targetTF.position);
            if (attackTime <= Time.time)
            {
                fsm.LookRotation(fsm.targetTF.position - fsm.transform.position);
                fsm.anim.SetBool(fsm.status.chPrams.attack, true);
                attackTime = Time.time + fsm.status.attackInterval;
            }
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fSMBase = fsm;
            fsm.GetComponentInChildren<AnimationEventBehaviour>().attackHandler += OnAttack;
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.GetComponentInChildren<AnimationEventBehaviour>().attackHandler -= OnAttack;
        }

        private void OnAttack()
        {
            fSMBase.targetTF.GetComponent<UnitController>().getDamaged(fSMBase.status.atk);
        }
    }
}
