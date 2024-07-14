using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CropFactory : Singleton<CropFactory>
{
    public enum CropType
    {
        Eggplant,
        Carrot,
        Tomato,
        WhiteRadish,
        Corn,
    }

    public GameObject CreateCrop(CropType cropType, Vector3 position, Quaternion rotation, int sortingLayer, int sortingOrder)
    {
        Debug.Log("Creating crop");
        GameObject crop = Instantiate(Resources.Load("Crop")) as GameObject;
        Debug.Log("Assets/Scripts/Crops/" + cropType.ToString() + "_CropData.asset");
        crop.GetComponent<Crop>().cropData = AssetDatabase.LoadAssetAtPath<CropData>("Assets/Scripts/Crops/" + cropType.ToString() + "_CropData.asset");
        crop.transform.SetPositionAndRotation(position, rotation);
        crop.GetComponent<SpriteRenderer>().sortingLayerID = sortingLayer;
        crop.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        return crop;
    }
}
