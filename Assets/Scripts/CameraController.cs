using UnityEngine;
 
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -8f);
 
    private void LateUpdate()
    {
        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}