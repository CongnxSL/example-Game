using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngAnim : MonoBehaviour
{
    void Start()
    {
        if (gameObject.CompareTag("Anim 1")) { Destroy(gameObject, 0.5f); }
        if (gameObject.CompareTag("Anim 2")) { Destroy(gameObject, 1); }
        if (gameObject.CompareTag("Anim 3")) { Destroy(gameObject, 1.5f); }
    }
}

