using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVIsual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private GameObject plateVisual;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;

        plateCounter.OnRemovePlate += PlateCounter_OnRemovePlate;
    }

    private void PlateCounter_OnRemovePlate(object sender, System.EventArgs e)
    {
        GameObject plateToRemove = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];

        plateVisualGameObjectList.Remove(plateToRemove);

        Destroy(plateToRemove);
    }

    private void PlateCounter_OnSpawnPlate(object sender, System.EventArgs e)
    {
        GameObject spawnedPlate = Instantiate(plateVisual, counterTopPoint);

        float plateOffSetY = 0.1f;

        spawnedPlate.transform.localPosition = new Vector3(0, plateOffSetY * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(spawnedPlate);
    }
}
