using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    [SerializeField] private GameObject playerHead;
    private Vector3 directionToLook;

    private void Update()
    {
        RotatePlayerHead();
    }

    private void RotatePlayerHead() {
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionToLook = mousePositionWorld - playerHead.transform.position;
        directionToLook.z = 0.0f;

        float angle = Mathf.Atan2(directionToLook.y, directionToLook.x) * Mathf.Rad2Deg;
        playerHead.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(playerHead.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
