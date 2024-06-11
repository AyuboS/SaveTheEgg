using UnityEngine;
using System.Collections;

public class HammerController : MonoBehaviour
{

    public float strikeSpeed;
    public float returnSpeed;
    public float strikeAngle = 0f;
    private Quaternion originalRotation;
    private Quaternion strikeRotation;
    private bool isStriking = false;
    private float strikeInterval = 3f;

    void Start()
    {
        strikeSpeed = Random.Range(210f, 230f);
        returnSpeed = Random.Range(80f, 100f);
        originalRotation = transform.rotation;
        strikeRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, strikeAngle);
        StartCoroutine(AutoStrike());
    }

    IEnumerator AutoStrike()
    {
        while (true)
        {
            yield return new WaitForSeconds(strikeInterval);
            if (!isStriking)
            {
                StartCoroutine(Strike());
            }
        }
    }

    IEnumerator Strike()
    {
        isStriking = true;
        float angleToStrike = Quaternion.Angle(transform.rotation, strikeRotation);
        float angleToOriginal = Quaternion.Angle(transform.rotation, originalRotation);

        while (angleToStrike > 0.01f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, strikeRotation, strikeSpeed * Time.deltaTime);
            angleToStrike = Quaternion.Angle(transform.rotation, strikeRotation);
            yield return null;
        }

        while (angleToOriginal > 0.01f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, returnSpeed * Time.deltaTime);
            angleToOriginal = Quaternion.Angle(transform.rotation, originalRotation);
            yield return null;
        }

        isStriking = false;
    }
}
