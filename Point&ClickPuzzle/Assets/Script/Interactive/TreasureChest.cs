using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//宝箱
public class TreasureChest : Interactive
{
    private SpriteRenderer _renderer;
    public Sprite OpenChest;

    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (isDone)
        {
            _renderer.sprite = OpenChest;
        }
    }
    
}
