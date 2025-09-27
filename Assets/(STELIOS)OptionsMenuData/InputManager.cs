using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;
    public bool BackButton { get; private set; }
    public bool EnterButton { get; private set; }
    public bool UpButton { get; private set; }
    public bool DownButton { get; private set; }
    public bool LeftButton { get; private set; }
    public bool RightButton { get; private set; }
    public bool InterractButton { get; private set; }
    public bool MapButton { get; private set; }
    public bool InventoryButton { get; private set; }
    public bool OpenPauseMenuButton { get; private set; }
    private PlayerInput _playerInput;
    private InputAction _BackButtonAction,  _EnterButtonAction, _UpButtonAction, _DownButtonAction, _LeftButtonAction, _RightButtonAction , _InterractButtonAction, _MapButtonAction, _InventoryButtonAction, _OpenPauseMenuButtonAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();
        _BackButtonAction = _playerInput.actions["BackButton"];
        _EnterButtonAction = _playerInput.actions["EnterButton"];
        _UpButtonAction = _playerInput.actions["UpButton"];
        _DownButtonAction = _playerInput.actions["DownButton"];
        _LeftButtonAction = _playerInput.actions["LeftButton"];
        _RightButtonAction = _playerInput.actions["RightButton"];
        _InterractButtonAction = _playerInput.actions["InterractButton"];
        _MapButtonAction = _playerInput.actions["MapButton"];
        _InventoryButtonAction = _playerInput.actions["InventoryButton"];
        _OpenPauseMenuButtonAction = _playerInput.actions["OpenPauseMenuButton"];
    }

    // Update is called once per frame
    private void Update()
    {
        BackButton = _BackButtonAction.WasPressedThisFrame();
        EnterButton = _EnterButtonAction.WasPressedThisFrame();
        UpButton = _UpButtonAction.WasPressedThisFrame();
        DownButton = _DownButtonAction.WasPressedThisFrame();
        LeftButton = _LeftButtonAction.WasPressedThisFrame();
        RightButton = _RightButtonAction.WasPressedThisFrame();
        InterractButton = _InterractButtonAction.WasPressedThisFrame();
        MapButton = _MapButtonAction.WasPressedThisFrame();
        InventoryButton = _InventoryButtonAction.WasPressedThisFrame();
        OpenPauseMenuButton = _OpenPauseMenuButtonAction.WasPressedThisFrame();
    }
}
