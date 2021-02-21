using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
