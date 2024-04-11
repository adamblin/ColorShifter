using UnityEngine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform mainPlayer;
    public float smoothSpeed = 0.125f;

    private Vector3 offset;
    private Vector3 desiredPosition;

    private void Start()
    {
        if (mainPlayer != null)
        {
            transform.position = mainPlayer.position;
            offset = transform.position - mainPlayer.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }

    private void LateUpdate()
    {
        if (mainPlayer != null)
        {
            desiredPosition = mainPlayer.position + offset;
            desiredPosition.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}


