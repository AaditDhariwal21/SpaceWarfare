using System;
using UnityEngine;

public abstract class SpecialController : MonoBehaviour
{  
    public abstract void Activate(Transform ship);

    public event Action OnFinished;
    protected void Finish()
    {
         OnFinished?.Invoke();
        Destroy(gameObject);
    }
}
