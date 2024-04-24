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

    private bool facingRight = true;
    private bool mouseRightSide = true;
    private bool canFlip = true;

    private void Update()
    {
        RotatePlayerHead();
        CheckMousePosition();
    }

    private void RotatePlayerHead() {
        if (canFlip) { 

            if (mouseRightSide != facingRight)
            {
                FlipPlayer();
            }

            mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get mouse direction
            mousePositionWorld.z = 0;

            directionToLook = (mousePositionWorld - playerHead.transform.position).normalized;

            playerHead.transform.right = directionToLook * Mathf.Sign(transform.localScale.x);
            var eulerDir = playerHead.transform.localEulerAngles;
            eulerDir.z = Mathf.Clamp(eulerDir.z - (eulerDir.z > 180 ? 360 : 0),
                maxBottomHeadAngle,
                maxTopHeadAngle);
            playerHead.transform.localEulerAngles = eulerDir;

        }
    }

    private void FlipPlayer() {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CheckMousePosition()
    {
        if (mousePositionWorld.x >= transform.position.x) //derecha
            mouseRightSide = true;
        else
            mouseRightSide = false;
    }

    private void CanNotFlip()
    {
        canFlip = false;
    }

    private void CanFlip()
    {
        canFlip = true;
    }

    private void OnEnable()
    {
        TongueController.Instance.onShootingTongue += CanNotFlip;
        TongueController.Instance.onNotMovingTongue += CanFlip;
    }

    private void OnDisable()
    {
        TongueController.Instance.onShootingTongue -= CanNotFlip;
        TongueController.Instance.onNotMovingTongue -= CanFlip;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(playerHead.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
