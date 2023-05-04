using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    [SerializeField] SpriteRenderer _cartSpriteRenderer;
    [SerializeField] Sprite _cartSprite;

    private void Start()
    {
        _cartSpriteRenderer.sprite = _cartSprite;
    }
}
