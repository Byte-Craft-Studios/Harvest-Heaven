using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 camOffset;

    void Start()
    {
        FindPlayer();
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            camOffset = transform.position - target.position;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (target == null)
        {
            FindPlayer(); // Try to reassign if lost
            return;
        }

        transform.position = target.position + camOffset;
    }
}
