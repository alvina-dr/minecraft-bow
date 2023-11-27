using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBehavior : MonoBehaviour
{
    #region Variables
    public Rigidbody rigibody;
    [HideInInspector] public bool blockPlayerMovement;
    private float currentSpeed;

    [Header("STATS")]
    private float currentHealth;
    private float maxHealth = 10;

    [Header("MOVEMENT SYSTEM")]
    public float runningSpeed;
    public float walkingSpeed;
    public float sneakingSpeed;
    public float cameraSpeed;
    public float jumpForce;
    public bool jump = false;
    private Vector3 moveDirection;

    [Header("SHOOTING SYSTEM")]
    public Arrow arrowPrefab;
    private Arrow loadedArrow;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private float shootForce;
    private float shootTimer;
    private float minShootTimer = .01f;
    [SerializeField] private float maxShootTimer;
    [SerializeField] private float normalFOV;
    [SerializeField] private float focusFOV;
    [SerializeField] private Transform bow;
    private Vector2 startingPos;
    [SerializeField] private List<GameObject> bowModels;
    [SerializeField] private ItemData arrowData;

    [Header("INVENTORY")]
    public Inventory inventory;

    [Header("AUDIO COMPONENTS")]
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private AudioSource hitAudioSource;
    public AudioSource pickUpSound;
    #endregion

    private void Start()
    {
        currentSpeed = walkingSpeed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        shootTimer = 0;
        startingPos.x = bow.localPosition.x;
        startingPos.y = bow.localPosition.y;
        GPCtrl.Instance.UICtrl.inventoryBar.UpdateInventory(inventory);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GPCtrl.Instance.Pause();
        }
        if (GPCtrl.Instance.pause)
        {
            moveDirection = Vector3.zero;
            return;
        }
        if (Input.GetMouseButtonUp(0) && inventory.SearchItem(arrowData))
        {
            if (shootTimer >= minShootTimer) ShootArrow();
            else shootTimer = 0;
        }
        if (Input.GetMouseButton(0) && inventory.SearchItem(arrowData))
        {
            currentSpeed = sneakingSpeed;
            if (shootTimer < maxShootTimer)
            {
                shootTimer += Time.deltaTime;
                Camera.main.fieldOfView = Mathf.Lerp(normalFOV, focusFOV, shootTimer / maxShootTimer);
                ChangeBowModel(Mathf.RoundToInt(shootTimer / maxShootTimer * bowModels.Count));
            }
            if (shootTimer >= maxShootTimer)
            {
                bow.localPosition = new Vector3(bow.localPosition.x, startingPos.y + (Mathf.Sin(Time.deltaTime * .5f) * 1.1f), bow.localPosition.z);
            }
        } else
        {
            if (Input.GetKey(KeyCode.LeftShift)) currentSpeed = runningSpeed;
            else currentSpeed = walkingSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigibody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        } else
        {
            jump = false;
        }
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Camera.main.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * cameraSpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * moveDirection.z * currentSpeed + transform.right * moveDirection.x * currentSpeed;
        rigibody.velocity = new Vector3(movement.x, rigibody.velocity.y, movement.z);
    }

    private void ShootArrow()
    {
        shootAudioSource.Play();
        loadedArrow = Instantiate(arrowPrefab, arrowHolder);
        loadedArrow.ShootArrow(Camera.main.transform.forward * shootTimer / maxShootTimer * shootForce);
        shootTimer = 0;
        Camera.main.DOFieldOfView(normalFOV, .3f);
        bow.DOLocalMove(new Vector3(startingPos.x, startingPos.y, bow.localPosition.z), .3f);
        ChangeBowModel(0);
        inventory.RemoveItem(arrowData, 1);
    }

    private void ChangeBowModel(int _num)
    {
        for (int i = 0; i < bowModels.Count; i++)
        {
            bowModels[i].gameObject.SetActive(false);
            if (i == _num)
                bowModels[i].gameObject.SetActive(true);
            if (_num == bowModels.Count)
                bowModels[bowModels.Count-1].gameObject.SetActive(true);
        }
    }

    public void Damage(float _damage)
    {
        currentHealth -= _damage;
        GPCtrl.Instance.UICtrl.healthBar.SetBarValue(currentHealth, maxHealth);
        hitAudioSource.Play();
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        GPCtrl.Instance.GameOver();
    }
}
