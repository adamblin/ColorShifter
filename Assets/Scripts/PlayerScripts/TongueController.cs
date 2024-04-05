using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TongueController : MonoBehaviour
{
    [SerializeField] private Transform tongueEnd;
    [SerializeField] private Transform tongueOrigin;

    [SerializeField] private float tongueSpeed;
    [SerializeField] private float maxTongueDistance;
    [SerializeField] private float detectionRadius;

    private ColorType[] colorTypes;
    private int currentColorIndex = 0;

    private bool shootTongue = false;
    private bool canShootagain = true;
    private Vector3 shootDirection;

    private LineRenderer lineRenderer;
    private ColorManager colorManager;

    private void Start()
    {
        colorTypes = new ColorType[] {
            ColorType.Elastic,
            ColorType.Water,
            ColorType.Strech
        };

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        colorManager = FindAnyObjectByType<ColorManager>(); 
    }

    private void FixedUpdate()
    {
        //assignar posiciones al line renderer
        lineRenderer.SetPosition(0, tongueOrigin.position);
        lineRenderer.SetPosition(1, tongueEnd.position);

        ShootTongue();
        CheckTongueCollisions();
        CheckMaxTongueDistance();
    }

    private void ShootTongue() {
        if (shootTongue) {
            shootDirection = GetShootingDirection();
            tongueEnd.position += shootDirection * tongueSpeed;
        }
        else
        {
            if (Vector3.Distance(tongueOrigin.position, tongueEnd.position) != 0)
            {
                tongueEnd.position = Vector3.MoveTowards(tongueEnd.position, tongueOrigin.position, tongueSpeed);
            }
            else 
            {
                canShootagain = true;
            }
        }
    }

    private Vector3 GetShootingDirection() {
        //cal aplicar mes logica

        return transform.right;
    }


    private void CheckTongueCollisions() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(tongueEnd.position, detectionRadius);
        Debug.Log("tongue colliding: " + hitColliders.Length);

        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].gameObject.tag.Equals("Obstacle")) {
                shootTongue = false;
                break;
            }
            else if (hitColliders[i].gameObject.tag.Equals("PaintableObstacle")) {
                shootTongue = false;
                ChangeObjectEffect(hitColliders[i].gameObject);
            }
        }
    }


    private void ChangeObjectEffect(GameObject target) {
        IColorEffect currentEffect = colorManager.GetColorEffect(colorTypes[currentColorIndex]);
        ObstacleEffectLogic colorableObject = target.GetComponent<ObstacleEffectLogic>();
        colorableObject.ApplyEffect(currentEffect);
    }


    private void CheckMaxTongueDistance()
    {
        float currentDistance = Vector3.Distance(tongueOrigin.position, tongueEnd.position);
        Debug.Log(currentDistance);

        if (currentDistance >= maxTongueDistance || currentDistance <= -maxTongueDistance) {
            Debug.Log("PASSED MAX DISTANCE");
            shootTongue = false;
        }
    }


    private void setShootTongue() {
        if (canShootagain) { 
            shootTongue = true;
            canShootagain = false;
        }
    }
    


    private void OnEnable()
    {
        PlayerInputs.onShoot += setShootTongue;
    }

    private void OnDisable()
    {
        PlayerInputs.onShoot -= setShootTongue;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 3);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(tongueEnd.position, detectionRadius);
    }
}
