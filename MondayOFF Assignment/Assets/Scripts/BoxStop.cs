using UnityEngine;

public class BoxStop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pedestal"))
        {

        }
    }
}
