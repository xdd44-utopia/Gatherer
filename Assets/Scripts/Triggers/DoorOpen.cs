﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public void Open()
    {
        GetComponentInChildren<Animator>().SetBool("open", true);
        FindObjectOfType<AudioManager>().Play("DoorOpen", 1);
    }
}
