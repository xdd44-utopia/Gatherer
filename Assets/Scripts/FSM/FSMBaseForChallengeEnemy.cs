using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM
{
    ///<summary>
    ///精英敌人状态机
    ///</summary>
    public class FSMBaseForChallengeEnemy : FSMBase
    {
        private EnemySkills skills;
        
        public float attackInterval = 1;

        protected override void InitComponents()
        {
            base.InitComponents();
            skills = GetComponent<EnemySkills>();
            allTargetTF = new Dictionary<int, Transform>();
        }

        public override void Attack()
        {
            base.Attack();
            skills.UseSkill();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Unit")
            {
                if (!allTargetTF.ContainsKey(collision.GetInstanceID()))

                    allTargetTF.Add(collision.GetInstanceID(), collision.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Unit")
            {
                if (allTargetTF.ContainsKey(collision.GetInstanceID()))
                    allTargetTF.Remove(collision.GetInstanceID());
            }
        }
    }
}
