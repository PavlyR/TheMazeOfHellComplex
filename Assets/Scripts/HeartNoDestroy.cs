using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartNoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
