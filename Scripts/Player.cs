using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, InterfactKitchenObjectParent
{
    public static Player Instance { get; private set; }

    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private bool isWalking;
    private Vector3 lastInteract;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private PlateKitchenObject plateKitChenObject;


    public class SelectedCounterChangedColorArgs : EventArgs
    {
        public BaseCounter selectedCounterArgs;
    }

    public event EventHandler<SelectedCounterChangedColorArgs> OnSelectedCounterChangedColor;
    public event EventHandler OnPickup;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractActionAlternate += GameInput_OnInteractActionAlternate;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractActionPlate += GameInput_OnInteractActionPlate;
    }

    private void GameInput_OnInteractActionPlate(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        if(selectedCounter != null)
        {
            selectedCounter.InteractPlate(this);
        }
    }

    private void GameInput_OnInteractActionAlternate(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);

        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        //tr??ng h?p ng??i ch?i ?ang ch?m clearCounter
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);

        }
    }

    private void Update()
    {
        PlayerMovement();
        PlayerInteractions();
    }

    private void PlayerMovement()
    {
        float playerHeight = 2f;
        float playerRadius = 0.7f;
        float movementDistance = movementSpeed * Time.deltaTime;
        Vector3 movementDirection = gameInput.GetPlayerMovementInput();

        //kiem tra phia truoc co cham vao vat gi do khong de chan nguoi choi di xuyen 
        if (!IsCollideWithSomething(playerHeight, playerRadius, movementDirection, movementDistance))
        {
            //d�ng de nhan vat di chuyen
            MakePlayerMove(movementDirection);
        }
        else
        {
            //cho phep nhan vat co the di chuyen khi vua di chuyen phia truoc vua di chuyen 2 ben (co nghia la vua nhan nut WA hoac WD) trong khi bi chan boi vat gi do
            //khong the di chuyen phia truoc
            //thu di chuyen huong x
            Vector3 movementDirectionX = new Vector3(movementDirection.x, 0, 0);
            
            //kiem tra huong x co cham vao vat gi do khong
            if(!IsCollideWithSomething(playerHeight, playerRadius, movementDirectionX, movementDistance))
            {
                //cho nhan vat di chuyen huong x
                MakePlayerMove(movementDirectionX);
            }
            else
            {
                //khong the di chuyen huong x
                //thu di chuyen huong z
                Vector3 movementDirectionZ = new Vector3(0, 0, movementDirection.z);

                //kiem tra huong z co cham vao vat gi do khong
                if (!IsCollideWithSomething(playerHeight, playerRadius, movementDirectionZ, movementDistance))
                {
                    //cho nhan vat di chuyen huong z
                    MakePlayerMove(movementDirectionZ);
                }
            }
        }

     

        //d�ng de nhan vat xoay nguoi muot ma theo huong di
    MakePlayerRotate(movementDirection, rotationSpeed);

        //dung de kiem tra nhan vat co di chuyen khong
        if (movementDirection != Vector3.zero)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    //h�m n�y d�ng ?? khi ng??i ch?i t?i g?n clearCounter th� k�ch ho?t EvenHandler OnSelectedCounterChangedColor
    private void PlayerInteractions()
    {
        float interactDistance = 2f;
        Vector3 movementDirection = gameInput.GetPlayerMovementInput();

        //c�i n�y gi�p ch�ng ta v?n t??ng t�c v?i clearCounter khi ch�ng ta kh�ng di chuy?n h??ng t?i clearCounter
        //b?ng c�ch t?i g?n clearCounter va ??ng l?i (Vector3.zero ngh?a l� ng??i ch?i ?ang kh�ng di chuy?n) th� lastInteract s? l?y gi� tr? c?a movementDirection
        if (movementDirection != Vector3.zero)
        {
            lastInteract = movementDirection;
        }

        //ki?m tra c� v?t g� ph�a tr??c ng??i ch?i kh�ng
        //transform: l� 1 thu?c t�nh c?a m?i v?t th? (object), n� ch?a th�ng tin v? v? tr�, h??ng, t? l?...
        //this.transform.position: l� 1 ch?c n?ng c?a transform,n� tr? v? gi� tr? vector3 (v? tr�) c?a this (ng??i ch?i)
        //raycastHit: l� 1 tia ??n b?n ra d�ng ?? ki?m tra c� va ch?m v?i v?t th? n�o kh�ng
        if (Physics.Raycast(this.transform.position, lastInteract, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            //raycastHit.transform.TryGetComponent: ki?m tra v� l?y th�nh ph?n c?a clearCounter t? v?t th? b? raycastHit va ch?m
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //tr??ng h?p 1: ng??i ch?i b?t ??u t?i g?n 1 clearCounter, l�c n�y selectedCounter = null, khi ng??i ch?i ch?m v�o clearCounter
                //tr??ng h?p 2: ng??i ch?i ?� ch?m v�o 1 clearCounter, sau ?� chuy?n sang 1 clearCounter kh�c, khi ?� selectedCounter mang gi� tr? c?a clearCounter c?
                //ki?m tra clearCOunter m� ng??i ch?i ?ang ch?m c� kh�c v�i selectedCOunter
                if (baseCounter != selectedCounter)
                {
                    //h�m n�y l� ?? l?y gi� tr? clearCounter m� ng??i ch?i ?ang ch?m v� k�ch ho?t s? ki?n
                    SetSelectedCounter(baseCounter);
                }
                //if ? tr�n gi�p kh�ng l?p l?i li�n t?c SeletectedCounte, nh? v?y s? gi�p game kh�ng b? ch?m
            }
            //tr??ng h?p else n�y l� raycastHit ?ang ch?m v�o v?t th? kh�ng ph?i l� clearCounter (c� th? l� t??ng ho?c c�i g� ?� kh�c)
            else
            {
                SetSelectedCounter(null);
            }
        }
        //tr??ng h?p else n�y l� ng??i ch?i kh�ng ch?m v�o v?t th?
        else
        {
            SetSelectedCounter(null);
        }

    }

    //h�m n�y l� ?? l?y gi� tr? clearCounter m� ng??i ch?i ?ang ch?m v� k�ch ho?t s? ki?n
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        if (OnSelectedCounterChangedColor != null)
        {
            //k�ch ho?t s? ki?n OnSelectedCounterChangedColor
            //s? ki?n n�y l?u l?i gi� tr? c?a clearCounter m� ng??i ch?i ?ang ch?m
            //s? ki?n n�y ?ang ???c ??ng k� b�n class SelectedCounter
            OnSelectedCounterChangedColor(this, new SelectedCounterChangedColorArgs { selectedCounterArgs = selectedCounter });
        }
    }

    private bool IsCollideWithSomething(float playerHeight, float playerRadius, Vector3 movementDirection, float movementDistance)
    {
        //kiem tra co cham vao vat gi do khong de ngan chan nguoi choi di xuyen vat do
        bool isCollide = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirection, movementDistance);
        return isCollide;
    }

    private void MakePlayerMove(Vector3 movementDirection)
    {
        //d�ng de nhan vat di chuyen
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }

    private void MakePlayerRotate(Vector3 movementDirection, float rotationSpeed)
    {
        transform.forward = Vector3.Slerp(transform.forward, gameInput.GetPlayerMovementInput(), Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public Transform GetTransformKitchenObjectPoint()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null)
        {
            if(OnPickup != null)
            {
                OnPickup(this, EventArgs.Empty);
            }
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void DeleteKitchenObject()
    {
        kitchenObject = null;
    }
}
