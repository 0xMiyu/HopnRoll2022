using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphkms : MonoBehaviour
{
    
    void Update()
    {
        if (!save.graphlook) {
            Destroy(gameObject);
        }
    }
}
