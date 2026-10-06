using System.Collections.Generic;

using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int initialSize = 20;

    private readonly List<GameObject> objects = new();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        GameObject instance = Instantiate(objectPrefab, transform);
        instance.SetActive(false);
        objects.Add(instance);

        return instance;
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject instance in objects)
        {
            if (!instance.activeSelf)
            {
                return Activate(instance, position, rotation);
            }
        }
        return Activate(CreateObject(), position, rotation);
    }

    private GameObject Activate(GameObject instance, Vector3 position, Quaternion rotation)
    {
        instance.transform.SetPositionAndRotation(position, rotation);

        instance.SetActive(true);

        return instance;
    }
}
