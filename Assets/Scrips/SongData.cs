using UnityEngine;

[CreateAssetMenu(fileName = "NewSongData", menuName = "Music/Song Data")]
public class SongData : ScriptableObject
{
    public AudioClip songClip;
    public float bpm;
    public float[] beatTimings;
    public float[] xPositions;
}
