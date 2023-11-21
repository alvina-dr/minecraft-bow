using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBehavior : MonoBehaviour
{
    #region Variables
    public Rigidbody rigibody;
    private Vector3 moveDirection;
    [HideInInspector] public bool blockPlayerMovement;
    private float currentSpeed;

    [Header("MOVEMENT SYSTEM")]
    public float walkingSpeed;
    public float sneakingSpeed;
    public float cameraSpeed;
    public float jumpForce;

    [Header("SHOOTING SYSTEM")]
    public Arrow arrowPrefab;
    private Arrow loadedArrow;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private float shootForce;
    private float shootTimer;
    private float minShootTimer = .2f;
    [SerializeField] private float maxShootTimer;
    [SerializeField] private float normalFOV;
    [SerializeField] private float focusFOV;
    [SerializeField] private Transform bow;
    [SerializeField] private Vector2 startingPos;
    #endregion

    private void Start()
    {
        currentSpeed = walkingSpeed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        shootTimer = minShootTimer;
        startingPos.x = bow.localPosition.x;
        startingPos.y = bow.localPosition.y;
    }

    private void Update()
    {
        if (blockPlayerMovement)
        {
            moveDirection = Vector3.zero;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            LoadArrow();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ShootArrow();
        }
        if (Input.GetMouseButton(0) && loadedArrow != null)
        {
            currentSpeed = sneakingSpeed;
            if (shootTimer < maxShootTimer)
            {
                shootTimer += Time.deltaTime;
                Camera.main.fieldOfView = Mathf.Lerp(normalFOV, focusFOV, shootTimer / maxShootTimer);
            }
            if (shootTimer >= maxShootTimer)
            {
                bow.localPosition = new Vector3(bow.localPosition.x, startingPos.y + (Mathf.Sin(Time.deltaTime * .5f) * 1.1f), bow.localPosition.z);
            }
        } else
        {
            currentSpeed = walkingSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigibody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * cameraSpeed);
        Camera.main.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * cameraSpeed);
    }

    private void FixedUpdate()
    {
        rigibody.velocity = transform.forward * moveDirection.z * currentSpeed + transform.right * moveDirection.x * currentSpeed;
    }

    private void LoadArrow()
    {
        loadedArrow = Instantiate(arrowPrefab, arrowHolder);
    }

    private void ShootArrow()
    {
        if (loadedArrow != null)
        {
            loadedArrow.ShootArrow(Camera.main.transform.forward * shootTimer / maxShootTimer * shootForce);
            shootTimer = minShootTimer;
            Camera.main.DOFieldOfView(normalFOV, .3f);
            bow.DOLocalMove(new Vector3(startingPos.x, startingPos.y, bow.localPosition.z), .3f);
        }
    }
}
