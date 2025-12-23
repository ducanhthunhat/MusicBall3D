using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    // --- SỬA ĐỔI: Dùng Dictionary để lưu số lượng từng loại skin ---
    // Key: Tên Skin, Value: Số lượng
    public Dictionary<string, int> skinInventory = new Dictionary<string, int>();

    public bool isBossActive = false; // Biến kiểm soát Boss
    public ObjectPool objectPool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Hàm thêm skin và trả về số lượng hiện tại của loại đó
    public int AddSkin(string skinName)
    {
        if (skinInventory.ContainsKey(skinName))
        {
            skinInventory[skinName]++;
        }
        else
        {
            skinInventory.Add(skinName, 1);
        }
        return skinInventory[skinName];
    }
}