﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLDShield : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
            Destroy(gameObject);
    }
}
