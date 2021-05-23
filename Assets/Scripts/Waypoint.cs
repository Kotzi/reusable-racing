using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public Bounds getBounds()
    {
        return this.spriteRenderer.bounds;
    }
}
