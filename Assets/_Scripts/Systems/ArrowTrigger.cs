using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    public static event Action<string> CurrentArrow;
    private void OnTriggerEnter(Collider other)
    {
        CurrentArrow?.Invoke(other.tag);
    }
    private void OnTriggerExit(Collider collision)
    {
        CurrentArrow?.Invoke("4");
    }
}
