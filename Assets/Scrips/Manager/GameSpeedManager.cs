using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    public static GameSpeedManager Instance;

    public float CurrentSpeed = 7f;       // tốc độ ban đầu
    public float MaxSpeed = 20f;          // tốc độ tối đa
    public float SpeedIncreaseRate = 0.2f; // tăng mỗi giây

    private void Awake() => Instance = this;

    void Update()
    {
        // tăng tốc theo thời gian
        CurrentSpeed += SpeedIncreaseRate * Time.deltaTime;
        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 7f, MaxSpeed);
    }
}
