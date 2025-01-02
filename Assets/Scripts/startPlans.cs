using UnityEngine;

public class startPlans : MonoBehaviour
{
    [SerializeField] GameObject planespwner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        planespwner.SetActive(true);
    }
}
