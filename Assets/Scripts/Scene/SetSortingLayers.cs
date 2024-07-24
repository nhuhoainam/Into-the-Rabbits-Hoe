// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SetSortingLayers : MonoBehaviour
// {
//     // Array of all the edge colliders
//     public EdgeCollider2D edgeCollider;
//     public string fromLayer;
//     public string toLayer;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         var player = GameObject.Find("Player");
//         if (edgeCollider.IsTouching(player.GetComponent<Collider2D>()))
//         {
//             if (player.GetComponent<SpriteRenderer>().sortingLayerName == fromLayer)
//             {
//                 player.GetComponent<SpriteRenderer>().sortingLayerName = toLayer;
//             }
//         }
//     }
// }
