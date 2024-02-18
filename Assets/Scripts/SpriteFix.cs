using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFix : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] Transform player;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if(player.transform.position.y > transform.position.y){
            spriteRenderer.sortingOrder = 10;
        }
        else
            spriteRenderer.sortingOrder = 0;

    }
}
