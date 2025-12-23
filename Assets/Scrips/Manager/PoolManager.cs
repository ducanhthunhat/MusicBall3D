// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public class PoolManager : MonoBehaviour
// {
//     public static PoolManager Instance;

//     private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new();
//     private Dictionary<GameObject, GameObject> objToPrefab = new();

//     private void Awake()
//     {
//         Instance = this;
//     }

//     // ----------- SPAWN -----------
//     public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot, int initSize = 5)
//     {
//         if (!poolDictionary.ContainsKey(prefab))
//         {
//             CreateNewPool(prefab, initSize);
//         }

//         Queue<GameObject> queue = poolDictionary[prefab];
//         GameObject obj;

//         if (queue.Count > 0)
//         {
//             obj = queue.Dequeue();
//         }
//         else
//         {
//             obj = Instantiate(prefab);
//         }

//         objToPrefab[obj] = prefab;
//         obj.transform.SetPositionAndRotation(pos, rot);
//         obj.SetActive(true);

//         return obj;
//     }

//     // ----------- DESPAWN -----------
//     public void Despawn(GameObject obj)
//     {
//         if (!objToPrefab.ContainsKey(obj))
//         {
//             Debug.LogWarning("Object not in pool, destroying instead");
//             Destroy(obj);
//             return;
//         }

//         // **Kill mọi tween đang chạy trên object trước khi deactivate**
//         DOTween.Kill(obj, complete: false);

//         obj.SetActive(false);
//         GameObject prefab = objToPrefab[obj];
//         poolDictionary[prefab].Enqueue(obj);
//     }

//     // ----------- AUTO CREATE POOL -----------
//     private void CreateNewPool(GameObject prefab, int size)
//     {
//         Queue<GameObject> queue = new();

//         for (int i = 0; i < size; i++)
//         {
//             GameObject obj = Instantiate(prefab);
//             obj.SetActive(false);
//             queue.Enqueue(obj);
//             objToPrefab[obj] = prefab;
//         }

//         poolDictionary[prefab] = queue;
//     }
//     private void OnDisable()
//     {
//         DOTween.Kill(gameObject);
//     }
// }
