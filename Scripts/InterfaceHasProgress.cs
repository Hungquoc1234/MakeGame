using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface InterfaceHasProgress
{
    public class OnProgressChangeEventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;
}
