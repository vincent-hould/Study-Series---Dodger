using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private bool isEnabled;
    [SerializeField] private float spawnDelay;

    private Camera cam;

    private void Start()
    {
        PlayerEvents.OnPlayerDeath += Disable;

        cam = Camera.main;
        StartCoroutine(Spawn());
    }

    private void Disable()
    {
        isEnabled = false;
    }

    private IEnumerator Spawn()
    {
        while (isEnabled) {
            GameObject spawnPrefab = GetRandomSpawn();
            Vector3 spawnPoint = GetSpawnPosition(spawnPrefab);
            GameObject spawn = Instantiate(spawnPrefab, spawnPoint, Quaternion.identity);
            spawn.transform.parent = transform;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private GameObject GetRandomSpawn()
    {
        int spawnIndex = Random.Range(0, spawns.Length - 1);
        return spawns[spawnIndex];
    }

    /**
     * Returns a random spawn point off-screen above the camera taking into account the spawn's sprite size 
     */
    private Vector3 GetSpawnPosition(GameObject spawn)
    {
        SpriteRenderer sr = spawn.GetComponent<SpriteRenderer>();
        float spriteWidth = sr ? sr.bounds.size.x : 0f;
        float spriteHeight = sr ? sr.bounds.size.y : 0f;
        float minX = cam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + (spriteWidth / 2f);
        float maxX = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - (spriteWidth / 2f);
        float x = Random.Range(minX, maxX);
        float y = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + (spriteHeight / 2f);
        return new Vector3(x, y, 0f);
    }
}
