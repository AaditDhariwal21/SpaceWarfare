using System;
using UnityEngine;

public abstract class SpecialController : MonoBehaviour
{
    public abstract void Activate(Transform ship);

    protected void Finish()
    {
        Destroy(gameObject);
    }
}
