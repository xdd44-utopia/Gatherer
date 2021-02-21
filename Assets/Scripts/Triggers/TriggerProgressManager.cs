using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerProgressManager : MonoBehaviour
{
    private SpriteRenderer[] renderers;
    private TriggerDetector detector;
    private TriggerManager manager;
    public Color color;

    private void Start()
    {
        renderers = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
        detector = transform.parent.GetComponent<TriggerDetector>();
        manager = transform.parent.parent.GetComponent<TriggerManager>();
    }

    private void Update()
    {
        if (detector.unitCount > manager.requiredNumber/manager.allTriggers.Length) return;

        for (int i = 0; i < detector.unitCount; i++)
        {
            renderers[i].color = color;
        }
    }
}
