using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    public GameObject dropletPrefab;

    void Start()
    {
        GameObject droplet = Instantiate(dropletPrefab, transform.position, Quaternion.identity);
        
        // droplet에 spawn 위치 전달
        var script = droplet.GetComponent<WaterDroplet>();
        if (script != null)
        {
            script.spawnPoint = transform;
        }
    }
}
