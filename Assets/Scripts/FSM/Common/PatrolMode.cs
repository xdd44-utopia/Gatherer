using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///巡逻状态
    ///</summary>
    public enum PatrolMode
    {
        /// <summary>
        /// 单次
        /// </summary>
        Once,
        /// <summary>
        /// 循环
        /// </summary>
        Loop,
        /// <summary>
        /// 往返
        /// </summary>
        PingPong
    }
}
