using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private CharacterController controller;
    private PlayerMovementManager inputManager;
    private Vector2 moveInput;
    private Vector2 aimInput;

    public float movementSpeed = 2f;
    public float rotationSpeed = 100f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputManager = new PlayerMovementManager();
    }

    private void Update()
    {
        MoveProcess();
    }

    private void LateUpdate()
    {
        LookProcess();
    }

    private void OnEnable()
    {
        inputManager.MovementInput.Enable();
        // Hareket girdisi
        inputManager.MovementInput.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputManager.MovementInput.Move.canceled += ctx => moveInput = Vector2.zero;
        // Fare girdisi
        inputManager.MovementInput.Aim.started += ctx => aimInput = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        inputManager.MovementInput.Disable();
    }

    private void MoveProcess()
    {
        // moveInput girdisi burada iþleniyor
        Vector3 move = Vector3.zero;
        move += new Vector3(-moveInput.x, 0, -moveInput.y);
        controller.Move(movementSpeed * Time.deltaTime * move);
    }

    private void LookProcess()
    {
        // aimInput girdisi burada iþleniyor
        // Fare ekran koordinatýný dünya koordinatýna çevir
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.y)
        );

        // Yalnýzca yatay düzlemde yön belirle
        Vector3 direction = mouseWorld - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
        }
    }
}
