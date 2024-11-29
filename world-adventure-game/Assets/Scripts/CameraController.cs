using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerBehavior player;
    [SerializeField] private Transform playerPos;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerBehavior>();
    }

    private void Update()
    {
        transform.position = new Vector3(playerPos.position.x, transform.position.y, transform.position.z);
    }
}
