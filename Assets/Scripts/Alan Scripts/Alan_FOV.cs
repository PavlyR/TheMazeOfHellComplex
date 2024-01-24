using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alan_FOV : MonoBehaviour
{
    public float radius;
    public float angle;

    public GameObject playerOb;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    public bool spotted;
}
