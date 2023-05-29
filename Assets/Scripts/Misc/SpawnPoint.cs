using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint spawnPoint;

    private void Start()
    {
        spawnPoint = this;
    }
}
