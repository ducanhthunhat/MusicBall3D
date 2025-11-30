using System.Collections;
using UnityEngine;

public class BallPlayerController : MonoBehaviour
{
    public float mouseSpeed = 10f;
    public float minHeight = 1f;
    public float maxHeight = 3f;
    public float maxDistance = 5f;
    public float xTolerance = 0.5f;
    public float jumpSpeed = 5f;

    private Transform[] tiles;
    private int currentTileIndex = 0;

    public void SetTiles(Transform[] spawnedTiles)
    {
        tiles = spawnedTiles;
        StartCoroutine(JumpThroughTiles());
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        transform.Translate(mouseX, 0, 0);
        Debug.Log("Mouse X: " + mouseX);
    }

    IEnumerator JumpThroughTiles()
    {
        if (tiles == null || tiles.Length == 0)
            yield break;

        while (currentTileIndex < tiles.Length)
        {
            Transform nextTile = tiles[currentTileIndex];

            if (Mathf.Abs(transform.position.x - nextTile.position.x) > xTolerance)
            {
                yield return null;
                continue;
            }

            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(transform.position.x, transform.position.y, nextTile.position.z);

            float distance = Vector3.Distance(startPos, endPos);
            float jumpHeight = Mathf.Lerp(minHeight, maxHeight, distance / maxDistance);
            float duration = distance / jumpSpeed;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                Vector3 pos = Vector3.Lerp(startPos, endPos, t);
                pos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;
                transform.position = pos;
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
            currentTileIndex++;

            // Kiểm tra nếu đã nhảy hết tile
            if (currentTileIndex >= tiles.Length)
            {
                WinGame();
            }
        }
    }

    void WinGame()
    {
        Debug.Log("🎉 Win Game!");
    }
}
