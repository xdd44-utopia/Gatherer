using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerProgressManager : MonoBehaviour
{
    private SpriteRenderer[] renderers;
    private TriggerManager manager;
    public Color color;

    private void Start()
    {
        renderers = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
        manager = transform.parent.parent.GetComponent<TriggerManager>();
    }

    private void Update()
    {
        if (manager.totalCount > 6) return;

        for (int i = 0; i < manager.totalCount; i++)
        {
            renderers[i].color = color;
        }
    }
}
