using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CropFactory : MonoBehaviour
{
    public enum CropType
    {
        Eggplant,
        Carrot,
        Tomato,
        WhiteRadish,
        Corn,
    }
    public static CropFactory Instance { get; private set;}
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject CreateCrop(CropType cropType, Vector3 position, Quaternion rotation)
    {
        Debug.Log("Creating crop");
        GameObject crop = Instantiate(Resources.Load("Crop")) as GameObject;
        Debug.Log("Assets/Scripts/Crops/" + cropType.ToString() + "_CropData.asset");
        crop.GetComponent<Crop>().cropData = AssetDatabase.LoadAssetAtPath<CropData>("Assets/Scripts/Crops/" + cropType.ToString() + "_CropData.asset");
        crop.transform.SetPositionAndRotation(position, rotation);
        crop.GetComponent<SpriteRenderer>().sortingOrder = 5;
        return crop;
    }
}
