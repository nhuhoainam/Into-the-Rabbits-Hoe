using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class FruitDropping : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.down, ForceMode2D.Impulse);
    }
    
}

public class Bush : MonoBehaviour, IPlayerInteractable
{
    private float FruitSpawnChance => bushData.FruitSpawnChance;
    [SerializeField] private bool hasFruit = true;
    static readonly float CheckSpawnInterval = 20;

    private float lastCheckTime = 0;
    [SerializeField] private BushData bushData;

    private bool isDroppingFruit = false;

    List<GameObject> fruitList = new();

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (hasFruit)
        {
            spriteRenderer.sprite = bushData.BushWithFruitSprite;
        }
        else
        {
            spriteRenderer.sprite = bushData.BushSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFruit && Time.time - lastCheckTime > CheckSpawnInterval)
        {
            lastCheckTime = Time.time;
            if (!hasFruit && Random.value < FruitSpawnChance)
            {
                spriteRenderer.sprite = bushData.BushWithFruitSprite;
                hasFruit = true;
            }
        }
    }

    void Harvest()
    {
        spriteRenderer.sprite = bushData.BushSprite;
        hasFruit = false;
        StartCoroutine(PlayFruitDropAnimation());
    }

    void IPlayerInteractable.Interact(PlayerData playerData)
    {
        if (hasFruit)
        {
            Debug.Log("Harvesting");
            Harvest();
        }
    }

    GameObject CreateFruit(Vector2 vel)
    {
        GameObject fruit = new GameObject();
        fruit.AddComponent<SpriteRenderer>().sprite = bushData.FruitSprite;
        fruit.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder + 1;
        fruit.GetComponent<SpriteRenderer>().sortingLayerID = this.GetComponent<SpriteRenderer>().sortingLayerID;
        fruit.transform.position = this.transform.position;
        fruit.AddComponent<Rigidbody2D>().velocity = vel;
        fruit.GetComponent<Rigidbody2D>().mass = 2f;
        return fruit;
    }

    IEnumerator PlayFruitDropAnimation() {
        List<Vector2> vels = new()
        {
            new Vector2(1, 5),
            new Vector2(1, 7),
            new Vector2(-1, 6),
        };
        List<GameObject> fruitList = new();
        foreach (var vel in vels)
        {
            var fruit = CreateFruit(vel);
            fruit.AddComponent<FruitDropping>();
            fruitList.Add(fruit);
        }
        yield return new WaitForSeconds(0.5f);
        foreach (var fruit in fruitList)
        {
            var trans = fruit.GetComponent<Transform>().transform;
            Destroy(fruit);
            ItemSpawner.Instance.SpawnItem(bushData.FruitItem.itemID, trans.position);
        }
    }

}
