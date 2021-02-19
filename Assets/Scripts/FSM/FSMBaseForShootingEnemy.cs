using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM
{
    ///<summary>
    ///射击敌人状态机
    ///</summary>
    public class FSMBaseForShootingEnemy : FSMBase
    {
        [HideInInspector]
        public Gun gun;
        protected override void InitComponents()
        {
            base.InitComponents();
            gun = GetComponent<Gun>();     
        }

        public override void Attack()
        {
            base.Attack();
            gun.Fire(targetTF.position);
        }

        
    }
}
