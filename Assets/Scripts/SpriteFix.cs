using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFix : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform player;
    [SerializeField] private int lowLayer = 5, highLayer = 10; 

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if(player.transform.position.y > transform.position.y){
            spriteRenderer.sortingOrder = highLayer;
        }
        else
            spriteRenderer.sortingOrder = lowLayer;

    }
}
