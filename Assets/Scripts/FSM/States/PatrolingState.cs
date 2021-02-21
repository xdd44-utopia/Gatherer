using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///巡逻状态
    ///</summary>
    public class PatrolingState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Patroling;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.isPatrolComplete = false;
            fsm.anim.SetBool(fsm.status.chPrams.move, true);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(fsm.status.chPrams.move, false);
        }

        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            Patroling(fsm);
        }

        public void Patroling(FSMBase fsm)
        {
            switch (fsm.patrolMode)
            {
                case PatrolMode.Once:
                    PatrolOnce(fsm);
                    break;
                case PatrolMode.Loop:
                    PatrolLoop(fsm);
                    break;
                case PatrolMode.PingPong:
                    PatrolPingPong(fsm);
                    break;
                case PatrolMode.OnlyOne:
                    PatrolOnlyOnce(fsm);
                    break;
                default:
                    break;
            }
        }

        private int currentIndex = -1;
        private void PatrolOnlyOnce(FSMBase fsm)
        {
            int randomIndex;
            while (true)
            {
                randomIndex = UnityEngine.Random.Range(0, fsm.wayPoints.Length);
                if (currentIndex != randomIndex) break;
            }
            currentIndex = randomIndex;
            fsm.MoveToTarget(fsm.wayPoints[currentIndex], 0, fsm.walkSpeed);
        }

        private int index;

        private void PatrolPingPong(FSMBase fsm)
        {
            //—— 往返 A B C B A
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) <= 0.5f)
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    //数组反转
                    Array.Reverse(fsm.wayPoints);
                    index++;
                }
                index = (index + 1) % fsm.wayPoints.Length;
            }
            //fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
            fsm.MoveToTarget(fsm.wayPoints[index], 0, fsm.walkSpeed);
        }

        private void PatrolLoop(FSMBase fsm)
        {
            //循环 A B C A B C
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) <= 0.5f)
            {
                index = (index + 1) % fsm.wayPoints.Length;
            }
            //fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
            fsm.MoveToTarget(fsm.wayPoints[index], 0, fsm.walkSpeed);
        }

        private void PatrolOnce(FSMBase fsm)
        {
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) <= 0.5f)
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    fsm.isPatrolComplete = true;
                    return;
                }
                index++;
            }

            //fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
            fsm.MoveToTarget(fsm.wayPoints[index], 0, fsm.walkSpeed);
        }
    }
}
