using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMove : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float gravity;

    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        ReadInput();
        ApplyGravity();
        MoveCharacter();
        CheckAnimation();
        RotateCharacter();
    }

    private void ReadInput()
    {
        moveDirection = playerInput.pc.Move.ReadValue<Vector3>();
    }

    private void MoveCharacter()
    {
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        movement.y = gravity;
        characterController.Move(movement);
    }

    private void RotateCharacter()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion rotate = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotate, rotationSpeed * Time.deltaTime);
        }
    }
    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            gravity += Physics.gravity.y * Time.deltaTime;
        }
        else 
        {
            gravity = 0f;
        }
    }

    private void CheckAnimation()
    {
        if(moveDirection != Vector3.zero)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
}
