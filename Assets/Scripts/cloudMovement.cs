using UnityEngine;

public class cloudMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public Vector3 direction = Vector3.left;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
