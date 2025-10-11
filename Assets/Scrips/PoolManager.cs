using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private List<GameObject> Tiles;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private Transform poolParent;

    void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        Tiles = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objPrefab, poolParent);
            obj.SetActive(false);
            Tiles.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (var obj in Tiles)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject newObj = Instantiate(objPrefab, poolParent);
        newObj.SetActive(false);
        Tiles.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }

}
