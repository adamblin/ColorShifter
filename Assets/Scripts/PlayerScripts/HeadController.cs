using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class HeadController : MonoBehaviour
{
    [SerializeField] private float maxTopHeadAngle;
    [SerializeField] private float maxBottomHeadAngle;

    [SerializeField] private GameObject playerHead;
    private Vector3 directionToLook;
    private Vector3 mousePositionWorld;

    private void Update()
    {
        RotatePlayerHead();
    }

    private void RotatePlayerHead() {
        mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionWorld.z = 0;

        directionToLook = (mousePositionWorld - playerHead.transform.position).normalized;

        Debug.Log(playerHead.transform.right);

        playerHead.transform.right = directionToLook * Mathf.Sign(transform.localScale.x);
        var eulerDir = playerHead.transform.localEulerAngles;
        eulerDir.z = Mathf.Clamp(eulerDir.z - (eulerDir.z > 180 ? 360 : 0), 
            maxBottomHeadAngle, 
            maxTopHeadAngle);
        playerHead.transform.localEulerAngles = eulerDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(playerHead.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
