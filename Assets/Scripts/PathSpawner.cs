using UnityEngine;
using System.Collections.Generic;

// PathSpawner: Handles spawning, moving, and deleting paths dynamically
public class PathSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pathPrefab;  // Prefab for the path object
    [SerializeField] private Transform playerTransform;  // Player's Transform reference
    [SerializeField] private int initialPaths = 3;  // Number of paths to spawn initially
    [SerializeField] private float pathLength = 215f;  // Length of each path segment

    private Queue<GameObject> activePaths = new Queue<GameObject>();  // Queue to track active paths
    private float spawnZPosition = 0f;  // Z position for the next path to spawn
    private float deleteDistance = 300f;  // Distance after which paths behind the player will be deleted

    private void Start()
    {
        // Spawn initial path segments
        for (int i = 0; i < initialPaths; i++)
        {
            SpawnPath();
        }
    }

    private void Update()
    {
        // Spawn a new path if the player approaches the end of the current paths
        if (playerTransform.position.z > spawnZPosition - (initialPaths * pathLength))
        {
            SpawnPath();
        }

        // Delete paths that are far behind the player
        if (activePaths.Count > 0 && playerTransform.position.z - activePaths.Peek().transform.position.z > deleteDistance)
        {
            DeletePath();
        }
    }

    private void SpawnPath()
    {
        // Instantiate a new path and add it to the queue
        GameObject path = Instantiate(pathPrefab, new Vector3(0, 0, spawnZPosition), Quaternion.identity);
        activePaths.Enqueue(path);
        spawnZPosition += pathLength;  // Update the spawn position for the next path
    }

    private void DeletePath()
    {
        // Remove the oldest path from the queue and destroy it
        GameObject oldPath = activePaths.Dequeue();
        Destroy(oldPath);
    }
}
