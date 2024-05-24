using System.Collections.Generic;
using UnityEngine;
using System;

public class SlotsSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private Transform parant;

    public Action<GameObject> OnAddObject;
    public Action<GameObject> OnRemoveObject;

    public Transform Parant => parant;

    public List<GameObject> Objects => objects;

    public void AddObject(GameObject obj)
    {
        if (objects.Contains(obj)) return;
        objects.Add(obj);
        OnAddObject?.Invoke(obj);
    }

    public void RemoveObject(GameObject obj)
    {
        if (!objects.Contains(obj)) return;
        objects.Remove(obj);
        OnRemoveObject?.Invoke(obj);
    }
}
