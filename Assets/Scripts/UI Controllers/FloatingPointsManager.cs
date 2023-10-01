using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Destroy(gameObject, 1f);
    }
    
}
