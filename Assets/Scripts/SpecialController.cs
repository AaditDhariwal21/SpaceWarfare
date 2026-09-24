using System;
using UnityEngine;

public abstract class SpecialController : MonoBehaviour
{
    public event Action OnFinished;

    public abstract void Activate(Transform owner);

    protected void Finish()
    {
        OnFinished?.Invoke();
        Destroy(gameObject);
    }
}
