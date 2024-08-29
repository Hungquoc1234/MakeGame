using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipScriptableObject audioClipScriptableObject;

    private float volume = 0.1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    void Start() 
    {
        Player.Instance.OnPickup += Instance_OnPickup;
        BaseCounter.OnPlaced += BaseCounter_OnPlaced;
        DeliveryManager.Instance.OnDeliverSuccessfully += Instance_OnDeliverSuccessfully;
        DeliveryManager.Instance.OnDeliverFailed += Instance_OnDeliverFailed;
        CuttingCounter.OnCuttingSound += CuttingCounter_OnCuttingSound;
        TrashCounter.OnTrash += TrashCounter_OnTrash;
        PlayerSound.OnWalking += PlayerSound_OnWalking;
    }

    private void PlayerSound_OnWalking(object sender, System.EventArgs e)
    {
        PlaySound(audioClipScriptableObject.footStep, Player.Instance.transform.position);
    }

    private void TrashCounter_OnTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipScriptableObject.trash, trashCounter.transform.position);

    }

    private void BaseCounter_OnPlaced(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipScriptableObject.dropObject, baseCounter.transform.position);
    }

    private void Instance_OnPickup(object sender, System.EventArgs e)
    {
        PlaySound(audioClipScriptableObject.pickupObject, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnCuttingSound(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipScriptableObject.chop, cuttingCounter.transform.position);
    }

    private void Instance_OnDeliverSuccessfully(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipScriptableObject.deliverySuccess, deliveryCounter.transform.position);
    }

    private void Instance_OnDeliverFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipScriptableObject.deliveryFail, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;

        if(volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
