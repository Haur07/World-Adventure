using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerMovement player;
    [SerializeField] private Transform playerPos;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        transform.position = new Vector3(playerPos.position.x, transform.position.y, transform.position.z);
    }
}
