using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Thông tin cơ bản")]
    public string levelName;
    public int levelIndex;
    public GameObject levelPrefab;
    public Sprite previewImage;

    [Header("Cài đặt gameplay")]
    public float timeLimit = 60f;
    public int enemyCount = 5;
    public int scoreToWin = 100;
}
