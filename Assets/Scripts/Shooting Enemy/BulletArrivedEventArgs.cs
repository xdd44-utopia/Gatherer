using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///子弹到达目标 事件参数类
///</summary>
public class BulletArrivedEventArgs
{
    public RaycastHit2D Hit { get; set; }
}
