using UnityEngine;

public class cutterManager : MonoBehaviour
{
    public Vector3 from;
    public Vector3 to;
    private float rotationSpeed;
    private float moveDuration;

    private float moveSpeed;
    private Vector3 moveDirection;

    private void Start()
    {
        rotationSpeed = Random.Range(-360f, 360f);
        moveDuration = Random.Range(3f, 6f);
        transform.position = from;
        moveDirection = (to - from).normalized;
        moveSpeed = Vector3.Distance(from, to) / moveDuration;
    }

    private void Update()
    {
        float moveDistance = Mathf.PingPong(Time.time * moveSpeed, Vector3.Distance(from, to));
        transform.position = from + moveDirection * moveDistance;

        Quaternion localRotation = Quaternion.Euler(rotationSpeed * Time.deltaTime, 0, 0);
        transform.localRotation *= localRotation;
    }
}
