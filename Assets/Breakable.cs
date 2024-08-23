using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Breakable : MonoBehaviour
{
    public List<GameObject> breakablePieces;
    public float timeToBreak = 2;
    private float timer = 0;
    public UnityEvent OnBreak;

    void Start()
    {
        foreach (var item in breakablePieces)
        {
            item.SetActive(false);
        }
    }

    public void Break()
    {
        timer += Time.deltaTime;

        if(timer < timeToBreak)
        {
            return;
        }

        foreach(var item in breakablePieces)
        {
            item.SetActive(true);
            item.transform.parent = null;
        }

        OnBreak.Invoke();

        gameObject.SetActive(false);
    }
}
