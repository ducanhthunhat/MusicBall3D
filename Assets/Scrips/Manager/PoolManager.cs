using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    public GameObject tilePrefab;
    public int poolSize = 10;
    public Transform poolParent;

    private List<GameObject> tiles;

    private void Awake() => Instance = this;

    private void Start()
    {
        tiles = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject t = Instantiate(tilePrefab, poolParent);
            t.SetActive(false);
            tiles.Add(t);
        }
    }

    public GameObject GetObject()
    {
        foreach (var t in tiles)
            if (!t.activeInHierarchy) return t;

        GameObject newT = Instantiate(tilePrefab, poolParent);
        newT.SetActive(false);
        tiles.Add(newT);
        return newT;
    }

    public void ReturnObject(GameObject t) => t.SetActive(false);
}
