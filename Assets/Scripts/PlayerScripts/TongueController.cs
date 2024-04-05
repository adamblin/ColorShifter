using System.Collections;
using System.Collections.Generic;
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
    }

    private void ShootTongue() {
        if (shootTongue)
        {
            shootDirection = transform.right;
            tongueEnd.position += shootDirection * tongueSpeed;
        }
        else 
        {
            if (tongueEnd.position != tongueOrigin.position) {
                shootDirection = -transform.right;
                tongueEnd.position += shootDirection * tongueSpeed;
            }
        }
    }

    private void CheckTongueCollisions() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(tongueEnd.position, detectionRadius);
        Debug.Log(hitColliders.Length);
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


    private void setShootTongue() {
        shootTongue = true;
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
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right.normalized * 1);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(tongueEnd.position, detectionRadius);
    }
}
