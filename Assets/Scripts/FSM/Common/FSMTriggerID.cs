using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///
    ///</summary>
    public enum FSMTriggerID
    {
        /// <summary>
        /// 没有生命
        /// </summary>
        NoHealth,
        /// <summary>
        /// 发现目标
        /// </summary>
        SawTarget,
        /// <summary>
        /// 到达目标
        /// </summary>
        ReachTarget,
        /// <summary>
        /// 丢失目标
        /// </summary>
        LoseTarget,
        /// <summary>
        /// 完成巡逻
        /// </summary>
        CompletePatrol,
        /// <summary>
        /// 目标被击杀
        /// </summary>
        TargetKilled,
        /// <summary>
        /// 超出攻击范围
        /// </summary>
        OutOfAttackRange
    }
}
