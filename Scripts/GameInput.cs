using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractActionAlternate;
    public event EventHandler OnInteractActionPlate;
    public event EventHandler OnPauseAction;

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Pause,
        Interact,
        Cut,
        Plate_Interact
    }

    PlayerInputAction playerInputAction;
    private void Awake()
    {
        

        Instance = this;

        playerInputAction = new PlayerInputAction();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputAction.Player.Enable();

        //khi ng??i ch?i b?m nút E thì kích ho?t hàm Interact_performed
        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputAction.Player.InteractPlate.performed += InteractPlate_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputAction.Player.InteractPlate.performed -= InteractPlate_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;

        playerInputAction.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(OnPauseAction != null)
        {
            OnPauseAction(this, EventArgs.Empty);
        }
    }

    private void InteractPlate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractActionPlate != null)
        {
            //s? ki?n này ?ang ???c ??ng ký bên class Player
            OnInteractActionPlate(this, EventArgs.Empty);
        }
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractActionAlternate != null)
        {
            //s? ki?n này ?ang ???c ??ng ký bên class Player
            OnInteractActionAlternate(this, EventArgs.Empty);
        }
    }

    //hàm này dùng ?? kích ho?t s? ki?n OnInteractAction
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(OnInteractAction != null)
        {
            //s? ki?n này ?ang ???c ??ng ký bên class Player
            OnInteractAction(this, EventArgs.Empty);
        }
    }

    public Vector3 GetPlayerMovementInput()
    {
        //cai nay la cach moi de nhan Input WSAD
        //sau khi tai xong package Input Action trong Unity, su dung Input Action de tao Input WSAD, sau do tu dong tao ra class InputAction
        //dung InputAction se giup code gon hon, co the them nhung Input khac va hoat dong binh thuong ma khong can phai dung den code
        //vd: sau khi tao xong Input WSAD, ta co the them Input <- ^ v -> (trai phai len xuong), nhu vay la player co the di chuyen voi WSAD va <- ^ v ->
        Vector3 inputVector = playerInputAction.Player.Move.ReadValue<Vector3>();

        //cai nay la cach cu de nhan Input WSAD
        //Vector3 inputVector = new Vector3(0,0,0);

        //dùng de kiem tra to hop phím WSAD de xác dinh huong di
        //if (Input.GetKey(KeyCode.W))
        //{
        //    inputVector.z = 1;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    inputVector.z = -1;

        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    inputVector.x = -1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputVector.x = 1;
        //}

        //dùng de nhân vat di huong cheo voi toc do bang huong doc ngang
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:

            case Binding.Move_Up:
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Down:
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case Binding.Move_Left:
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Right:
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.Interact:
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.Plate_Interact:
                return playerInputAction.Player.InteractPlate.bindings[0].ToDisplayString();
            case Binding.Cut:
                return playerInputAction.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void ReBinding(Binding binding)
    {
        playerInputAction.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputAction.Player.Move;

                bindingIndex = 3;

                break;

            case Binding.Move_Down:
                inputAction = playerInputAction.Player.Move;

                bindingIndex = 4;

                break;

            case Binding.Move_Left:
                inputAction = playerInputAction.Player.Move;

                bindingIndex = 1;

                break;

            case Binding.Move_Right:
                inputAction = playerInputAction.Player.Move;

                bindingIndex = 2;

                break;

            case Binding.Interact:
                inputAction = playerInputAction.Player.Interact;

                bindingIndex = 0;

                break;

            case Binding.Plate_Interact:
                inputAction = playerInputAction.Player.InteractPlate;

                bindingIndex = 0;

                break;

            case Binding.Cut:
                inputAction = playerInputAction.Player.InteractAlternate;

                bindingIndex = 0;

                break;

            case Binding.Pause:
                inputAction = playerInputAction.Player.Pause;

                bindingIndex = 0;

                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputAction.Player.Enable();



                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputAction.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            }).Start();
    }
}
