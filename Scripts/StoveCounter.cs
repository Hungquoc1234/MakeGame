using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter, InterfaceHasProgress
{
    [SerializeField] private FryRecipeScriptableObject fryRecipeScriptableObject;
    [SerializeField] private BurnRecipeScriptableObject burnRecipeScriptableObject;
    private float fryingTimer;
    private float burningTimer;

    public event EventHandler<OnStateChangeEventArgs> OnStateChange;

    public class OnStateChangeEventArgs
    {
        public State state;
    }

    public event EventHandler<InterfaceHasProgress.OnProgressChangeEventArgs> OnProgressChange;


    public enum State
    {
        DoingNothing,
        Frying,
        Fried,
        Burned
    }

    private State state;

    private void Start()
    {
        state = State.DoingNothing;
    }

    private void Update()
    {
        if(kitchenObject != null)
        {
            switch (state)
            {
                case State.DoingNothing:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    if (OnProgressChange != null)
                    {
                        OnProgressChange(this, new InterfaceHasProgress.OnProgressChangeEventArgs
                        {
                            progressNormalized = fryingTimer / fryRecipeScriptableObject.fryingTimerMax
                        });
                    }

                    if (fryingTimer > fryRecipeScriptableObject.fryingTimerMax)
                    {
                        kitchenObject.DestroyKitchenObject();

                        KitchenObject.SpawnKitchenObject(fryRecipeScriptableObject.output, this);

                        state = State.Fried;

                        if(OnStateChange != null)
                        {
                            OnStateChange(this, new OnStateChangeEventArgs
                            {
                                state = state
                            });
                        }

                        burningTimer = 0f;
                    }

                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;

                    if (OnProgressChange != null)
                    {
                        OnProgressChange(this, new InterfaceHasProgress.OnProgressChangeEventArgs
                        {
                            progressNormalized = burningTimer / burnRecipeScriptableObject.burningTimerMax
                        });
                    }

                    if (burningTimer > burnRecipeScriptableObject.burningTimerMax)
                    {
                        kitchenObject.DestroyKitchenObject();

                        KitchenObject.SpawnKitchenObject(burnRecipeScriptableObject.output, this);

                        state = State.Burned;

                        if (OnStateChange != null)
                        {
                            OnStateChange(this, new OnStateChangeEventArgs
                            {
                                state = state
                            });
                        }
                    }

                    break;

                case State.Burned:
                    break;
            }
            
        }
    }

    public override void Interact(Player player)
    {
        if (this.kitchenObject == null)
        {
            if (player.GetKitchenObject() != null)
            {
                if(HasRecipe(player.GetKitchenObject().GetKitchenObjectScriptableObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    state = State.Frying;

                    if (OnStateChange != null)
                    {
                        OnStateChange(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });
                    }

                    fryingTimer = 0f;

                    if (OnProgressChange != null)
                    {
                        OnProgressChange(this, new InterfaceHasProgress.OnProgressChangeEventArgs
                        {
                            progressNormalized = fryingTimer / fryRecipeScriptableObject.fryingTimerMax
                        });
                    }
                }
            }
        }
        else
        {
            if (player.GetKitchenObject() == null)
            {
                state = State.DoingNothing;

                if (OnStateChange != null)
                {
                    OnStateChange(this, new OnStateChangeEventArgs
                    {
                        state = state
                    });
                }

                if (OnProgressChange != null)
                {
                    OnProgressChange(this, new InterfaceHasProgress.OnProgressChangeEventArgs
                    {
                        progressNormalized = 0
                    });
                }

                this.kitchenObject.SetKitchenObjectParent(player);
            }
        }
    }

    private bool HasRecipe(KitchenObjectScriptableObject input)
    {
        if (fryRecipeScriptableObject.input == input)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void InteractPlate(Player player)
    {
        if (player.GetKitchenObject() != null)
        {
            //nguoi choi dang cam dia
            if (player.GetKitchenObject() is PlateKitchenObject)
            {
                //co vat khac tren clearcounter
                if (this.kitchenObject != null)
                {
                    PlateKitchenObject plateKitChenObject = player.GetKitchenObject() as PlateKitchenObject;

                    if (plateKitChenObject.PutKitchenObjectOnPlate(this.GetKitchenObject().GetKitchenObjectScriptableObject()))
                    {
                        this.GetKitchenObject().DestroyKitchenObject();
                    }

                    state = State.DoingNothing;

                    if (OnStateChange != null)
                    {
                        OnStateChange(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });
                    }

                    if (OnProgressChange != null)
                    {
                        OnProgressChange(this, new InterfaceHasProgress.OnProgressChangeEventArgs
                        {
                            progressNormalized = 0
                        });
                    }

                } 
            }
        }
    }
}
