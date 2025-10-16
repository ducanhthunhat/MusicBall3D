using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

#if UNITY_EDITOR
[CustomEditor(typeof(SongData))]
public class BeatAnalyzer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SongData data = (SongData)target;

        if (GUILayout.Button("🔍 Analyze Beat (Approx)"))
        {
            if (data.songClip == null)
            {
                Debug.LogError("⚠️ Hãy gán AudioClip trước!");
                return;
            }

            float secondsPerBeat = 60f / data.bpm;
            int totalBeats = Mathf.FloorToInt(data.songClip.length / secondsPerBeat);

            List<float> timings = new List<float>();
            List<float> positions = new List<float>();
            float[] xOptions = { -2f, 0f, 2f };

            for (int i = 0; i < totalBeats; i++)
            {
                timings.Add(i * secondsPerBeat);
                positions.Add(xOptions[Random.Range(0, xOptions.Length)]);
            }

            data.beatTimings = timings.ToArray();
            data.xPositions = positions.ToArray();
            EditorUtility.SetDirty(data);

            Debug.Log($"✅ Generated {totalBeats} beats for {data.songClip.name}");
        }
    }
}
#endif
