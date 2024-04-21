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

    private bool facingRight;
    private bool mouseRightSide;

    private void Update()
    {
        RotatePlayerHead();
        CheckMousePosition();
        CheckIfFacingRight();
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

        if (mouseRightSide != facingRight) { }
            FlipPlayer(); //GIRAR SIEMPRE HACIA EL RATON O NO

    }

    private void FlipPlayer() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CheckMousePosition() {
        if (mousePositionWorld.x >= transform.position.x) //derecha
            mouseRightSide = true;
        else
            mouseRightSide = false;
    }

    private void CheckIfFacingRight() {
        if (transform.localScale.x >= 0) //right
            facingRight = true;
        else
            facingRight = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(playerHead.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
