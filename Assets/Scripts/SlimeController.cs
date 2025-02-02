using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed = 3f; 
    public float minDistanceToPlayer = 1f; 
    public Transform player; 

    private Vector2 direction;
    private Rigidbody2D slimeBody;

    void Start()
    {
        slimeBody = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > minDistanceToPlayer)
        {
            direction = (player.position - transform.position).normalized;

            slimeBody.MovePosition(slimeBody.position + direction * Time.fixedDeltaTime);
        }
    }
}