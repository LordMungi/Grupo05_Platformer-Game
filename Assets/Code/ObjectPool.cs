using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject Object;

    private Stack<GameObject> _objectPool = new Stack<GameObject>();

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject output;
        if (_objectPool.Count > 0)
            output = _objectPool.Pop();

        else
            output = Instantiate(Object, position, rotation, transform);

        return output;
    }

    public GameObject Get(Vector3 position)
    {
        return Get(position, Quaternion.identity);
    }

    public GameObject Get()
    {
        return Get(Vector3.zero, Quaternion.identity);
    }

    public void Release(GameObject item)
    {
        item.SetActive(false);
        _objectPool.Push(item);
    }
}
