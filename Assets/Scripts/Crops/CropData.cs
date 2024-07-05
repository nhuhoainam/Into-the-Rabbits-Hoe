using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


[CreateAssetMenu]
public class CropData : ScriptableObject
{
    /// <summary>
    /// GrowthIntervals is a list of the number of in-game
    /// hours it takes for the crop to grow to the next stage.
    /// </summary>
    public List<int> GrowthIntervals;
    public List<Sprite> GrowthSprites;
    /// <summary>
    /// GrowthSpedUpIntervals is a list of the number of in-game hours it takes for
    /// the crop to grow to the next stage when the player uses a speed-up (fertilizer) item.
    /// </summary>
    public List<float> GrowthSpedUpIntervals;
    public int SellPrice;
    public int BuyPrice;

    void Awake()
    {
        Assert.IsTrue(GrowthIntervals.Count == GrowthSprites.Count, "GrowthIntervals and GrowthSprites must have the same number of elements.");
    }
}
