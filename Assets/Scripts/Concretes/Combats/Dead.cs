using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [SerializeField] int _delayTime = 3000;

    public void DeadAction()
    {
        DeadActionAsync();
    }

    private async void DeadActionAsync()
    {
         await Task.Delay(_delayTime);
        if(this == null) return;
        Destroy(this.gameObject);

    }
}
