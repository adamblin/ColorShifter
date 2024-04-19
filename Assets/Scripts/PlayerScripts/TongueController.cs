using UnityEngine;
using System;

public class TongueController : MonoBehaviour
{
    [Header("OTHER GAMEOBJECTS")]
    [SerializeField] private Transform tongueEnd;
    [SerializeField] private Transform tongueOrigin;

    [Header("TONGUE PARAMETERS")]
    [SerializeField] private float tongueSpeed;
    [SerializeField] private float maxTongueDistance;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask tongueCanCollide;

    private ColorType currentColorType;

    private bool shootTongue = false;
    private bool canShootAgain = true;
    private bool getDirectionAgain = true;
    private bool canCheckCollisions = true;
    private bool inWater = false;

    private Vector3 firstDirection;

    public static event Action onShootingTongue;
    public static event Action onNotMovingTongue;

    private LineRenderer lineRenderer;
    private ColorManager colorManager;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        colorManager = FindAnyObjectByType<ColorManager>(); 

        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangePlayerColor(ColorType.Default);
    }

    private void FixedUpdate()
    {
        lineRenderer.SetPosition(0, tongueOrigin.position);
        lineRenderer.SetPosition(1, tongueEnd.position);

        ShootTongue();
        CheckTongueCollisions();
        CheckMaxTongueDistance();
        ChangePlayerColor(currentColorType);
    }

    private void ShootTongue() {
        if (shootTongue) {
            Vector3 shootDirection = GetShootingDirection();
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
                canShootAgain = true;
                getDirectionAgain = true;
                canCheckCollisions = false;
                onNotMovingTongue?.Invoke();
            }
        }
    }

    private Vector3 GetShootingDirection() {
        if (getDirectionAgain) {
            Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            firstDirection = mousePositionWorld - transform.position;
            getDirectionAgain = false;
        }
        return firstDirection.normalized;
    }


    private void CheckTongueCollisions() {
        if (canCheckCollisions) {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(tongueEnd.position, detectionRadius, tongueCanCollide);

            for (int i = 0; i < hitColliders.Length; i++) {
;               if (hitColliders[i].gameObject.tag.Equals("Door")) {
                    hitColliders[i].gameObject.GetComponent<NextLevelDoor>().DoorCollided();
                }
                else if (hitColliders[i].gameObject.tag.Equals("PaintableObstacle")) {
                    ChangeObjectEffect(hitColliders[i].gameObject);
                }
                shootTongue = false;
                canCheckCollisions = false;
            }
        }
    }


    private void ChangeObjectEffect(GameObject target) {
        IColorEffect currentEffect = colorManager.GetColorEffect(currentColorType);
        target.GetComponent<ObstacleEffectLogic>().ApplyEffect(currentEffect);
    }


    private void CheckMaxTongueDistance()
    {
        float currentDistance = Vector3.Distance(tongueOrigin.position, tongueEnd.position);

        if (currentDistance >= maxTongueDistance || currentDistance <= -maxTongueDistance) {
            shootTongue = false;
        }
    }

    private void ChangePlayerColor(ColorType colorType) {
        currentColorType = colorType;
        spriteRenderer.color = colorManager.GetColor(currentColorType);
    }


    private void setShootTongue() {
        if (canShootAgain && !inWater) { 
            shootTongue = true;
            canShootAgain = false;
            canCheckCollisions = true;
            onShootingTongue?.Invoke();
        }
    }

    private void InWater() {
        inWater = !inWater;
    }

    private void OnEnable()
    {
        PlayerInputs.onShoot += setShootTongue;
        PlayerInputs.onChangeColor += ChangePlayerColor;
        WaterEffect.onWater += InWater;
        ColorManager.onGetColorBack += ChangePlayerColor;
    }

    private void OnDisable()
    {
        PlayerInputs.onShoot -= setShootTongue;
        PlayerInputs.onChangeColor -= ChangePlayerColor;
        WaterEffect.onWater -= InWater;
        ColorManager.onGetColorBack -= ChangePlayerColor;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 3);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(tongueEnd.position, detectionRadius);
    }
}
