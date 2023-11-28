using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [SerializeField] int _delayTime = 3000;
    public event Action IsDead;
    public void DeadAction()
    {
        DeadActionAsync();
    }

    private async void DeadActionAsync()
    {
        IsDead?.Invoke();
        await UniTask.Delay(_delayTime);
        if(this == null) return;
        Destroy(this.gameObject);

    }
}
