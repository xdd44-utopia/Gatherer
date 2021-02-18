using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///角色动画参数类
///</summary>
[Serializable] //序列化 将当前对象嵌入到脚本后，可在编译器中显示
public class CharacterAnimationParameter
{
    public string idle = "idle";
    public string attack = "attack";
    public string move = "move";
}
