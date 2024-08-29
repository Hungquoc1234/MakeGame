using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipScriptableObject : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFail;
    public AudioClip[] trash;
    public AudioClip[] warning;
    public AudioClip stovesizzle;
    public AudioClip[] dropObject;
    public AudioClip[] pickupObject;
    public AudioClip[] footStep;
}
