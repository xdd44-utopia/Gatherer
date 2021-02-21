using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyedGenerateUnits : MonoBehaviour
{
    private prop_splitedwall prop;
    private SpawnerController spawner;

    private void Awake()
    {
        prop = GetComponent<prop_splitedwall>();
        spawner = transform.parent.GetComponentInChildren<SpawnerController>();
    }

    private void OnEnable()
    {
        prop.WallDestroyedHandler += SpawnUnits;
    }

    private void SpawnUnits()
    {
        spawner.activate();
    }

    private void OnDisable()
    {
        prop.WallDestroyedHandler -= SpawnUnits;
    }
}
