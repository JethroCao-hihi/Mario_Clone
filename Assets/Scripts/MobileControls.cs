using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControls : MonoBehaviour
{
    public static MobileControls Instance { get; private set; }

    private float moveInput = 0f;
    private bool jumpPressed = false;
    private bool jumpConsumed = false;
    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }   
        Instance = this;
    }

    void Update()
    {
        UpdateMoveInput();
    }

    void LateUpdate()
    {
        if (jumpConsumed)
        {
            jumpPressed = false;
            jumpConsumed = false;
        }
    }

    private void UpdateMoveInput()
    {
        if (isLeftPressed)
        {
            moveInput = -1f;
        }
        else if (isRightPressed)
        {
            moveInput = 1f;
        }
        else
        {
            moveInput = 0f;
        }
    }

    public void OnLeftButtonDown()
    {
        Debug.Log("Left Button DOWN");
        isLeftPressed = true;
        isRightPressed = false;
    }

    public void OnLeftButtonUp()
    {
        Debug.Log("Left Button UP");
        isLeftPressed = false;
    }

    public void OnRightButtonDown()
    {
        Debug.Log("Right Button DOWN");
        isRightPressed = true;
        isLeftPressed = false;
    }

    public void OnRightButtonUp()
    {
        Debug.Log("Right Button UP");
        isRightPressed = false;
    }

    public void OnJumpButtonDown()
    {
        Debug.Log("Jump Button PRESSED");
        jumpPressed = true;
        jumpConsumed = false;
    }

    public float GetMoveInput()
    {
        return moveInput;
    }

    public bool GetJumpInput()
    {
        if (jumpPressed && !jumpConsumed)
        {
            Debug.Log("GetJumpInput returning TRUE");
            jumpConsumed = true;
            return true;
        }
        return false;
    }
}
