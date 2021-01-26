using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloattingText : MonoBehaviour
{
    public float DestroyTime = 2.0f;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

    
}
