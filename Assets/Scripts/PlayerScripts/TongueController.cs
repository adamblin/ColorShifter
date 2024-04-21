using UnityEngine;
using System;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class TongueController : MonoBehaviour
{
    //Singletone pattern
    private static TongueController instance;
    public static TongueController Instance {  
        get {
            if (instance == null)
                instance = FindAnyObjectByType<TongueController>();
            return instance;
        } }

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

    public event Action onShootingTongue;
    public event Action onNotMovingTongue;
    public event Action<Vector3> shootDirection;

    private LineRenderer lineRenderer;
    private ColorManager colorManager;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        //line renderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        //color manager
        colorManager = FindAnyObjectByType<ColorManager>(); 

        //sprite renderer
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
            tongueEnd.position += shootDirection * tongueSpeed * Time.fixedDeltaTime;
        }
        else
        {
            if (Vector3.Distance(tongueOrigin.position, tongueEnd.position) != 0)
            {
                tongueEnd.position = Vector3.MoveTowards(tongueEnd.position, tongueOrigin.position, tongueSpeed*Time.fixedDeltaTime);
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
        if (getDirectionAgain)
        {
            Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            firstDirection = mousePositionWorld - tongueOrigin.transform.position;
            firstDirection.z = 0.0f;
            getDirectionAgain = false;
        }
        shootDirection?.Invoke(firstDirection.normalized);
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

        if (currentDistance >= maxTongueDistance) {
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
