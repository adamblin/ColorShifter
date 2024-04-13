using UnityEngine;
using System;

public class TongueController : MonoBehaviour
{
    [SerializeField] private Transform tongueEnd;
    [SerializeField] private Transform tongueOrigin;

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
    private bool pointingUp = false;
    private bool pointingDown = false;
    private bool pointingStraight = false;

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
        //assignar posiciones al line renderer
        lineRenderer.SetPosition(0, tongueOrigin.position);
        lineRenderer.SetPosition(1, tongueEnd.position);

        ShootTongue();
        CheckTongueCollisions();
        CheckMaxTongueDistance();
        ChangePlayerColor(currentColorType);
    }

    private void ShootTongue() {
        if (shootTongue) {
            Debug.Log("Shooting tongue");
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

            if (pointingUp && !pointingStraight && !pointingDown)
                firstDirection = transform.up;

            else if (pointingUp && pointingStraight && !pointingDown)
                firstDirection = transform.up + transform.right;

            else if (pointingDown && !pointingStraight && !pointingUp)
                firstDirection = -transform.up;

            else if (pointingDown && pointingStraight && !pointingUp)
                firstDirection = -transform.up + transform.right;

            else
                firstDirection = transform.right;

            getDirectionAgain = false;
        }
        return firstDirection;
    }


    private void CheckTongueCollisions() {
        if (canCheckCollisions) {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(tongueEnd.position, detectionRadius, tongueCanCollide);

            for (int i = 0; i < hitColliders.Length; i++) {
                if (hitColliders[i].gameObject.tag.Equals("Door")) {
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
        Debug.Log("Shoot Tongue");
        if (canShootAgain && !inWater) { 
            shootTongue = true;
            canShootAgain = false;
            canCheckCollisions = true;
            onShootingTongue?.Invoke();
        }
    }

    private void setPointingUp(){
        pointingUp = !pointingUp;
        Debug.Log(pointingUp);
    }

    private void setPointingDown(){
        pointingDown = !pointingDown;
    }

    private void setPointingStraight(){
        pointingStraight = !pointingStraight;
    }

    private void InWater() {
        inWater = !inWater;
    }



    private void OnEnable()
    {
        PlayerInputs.onShoot += setShootTongue;
        PlayerInputs.onShootUp += setPointingUp;
        PlayerInputs.onShootDown += setPointingDown;
        PlayerInputs.onShootStraight += setPointingStraight;
        PlayerInputs.onChangeColor += ChangePlayerColor;
        WaterEffect.onWater += InWater;
        ColorManager.onGetColorBack += ChangePlayerColor;
    }

    private void OnDisable()
    {
        PlayerInputs.onShoot -= setShootTongue;
        PlayerInputs.onShootUp -= setPointingUp;
        PlayerInputs.onShootDown -= setPointingDown;
        PlayerInputs.onShootStraight -= setPointingStraight;
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
