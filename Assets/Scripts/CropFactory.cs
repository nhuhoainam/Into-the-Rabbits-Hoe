using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class CropFactory : Singleton<CropFactory>
{
    public enum CropType
    {
        Eggplant,
        Carrot,
        Corn,
        Pumpkin,
        Cauliflower,
        Cabbage,
    }

    readonly public static Dictionary<int, CropType> cropTypeDictionary = new()
    {
        { 7, CropType.Cabbage },
        { 8, CropType.Carrot },
        { 9, CropType.Cauliflower },
        { 10, CropType.Corn },
        { 11, CropType.Eggplant },
        { 12, CropType.Pumpkin },
    };


    public GameObject CreateCrop(CropType cropType, Vector3 position, Quaternion rotation, int sortingLayer, int sortingOrder)
    {
        Debug.Log("Creating crop " + cropType.ToString() + " at " + position.ToString() + " with sorting layer " + sortingLayer + " and sorting order " + sortingOrder + "...");
        GameObject crop = Instantiate(Resources.Load("Crop")) as GameObject;
        Debug.Log("Assets/Scripts/Crops/" + cropType.ToString() + "_CropData.asset");
        crop.GetComponent<Crop>().cropData = Resources.Load<CropData>("CropsData/" + cropType.ToString() + "_CropData");
        Assert.IsNotNull(crop.GetComponent<Crop>().cropData);
        crop.transform.SetPositionAndRotation(position, rotation);
        crop.GetComponent<SpriteRenderer>().sortingLayerID = sortingLayer;
        crop.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        return crop;
    }
}
