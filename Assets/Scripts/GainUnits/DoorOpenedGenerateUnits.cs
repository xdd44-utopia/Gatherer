using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenedGenerateUnits : MonoBehaviour
{
    private TriggerManager manager;
    private SpawnerController spawner;

    private void Awake()
    {
        manager = GetComponent<TriggerManager>();
        spawner = transform.GetComponentInChildren<SpawnerController>();
    }

    private void OnEnable()
    {
        manager.DoorOpenedHandler += SpawnUnits;
    }

    private void SpawnUnits()
    {
        spawner.activate();
    }

    private void OnDisable()
    {
        manager.DoorOpenedHandler -= SpawnUnits;
    }
}
