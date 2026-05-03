using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Hit()
    {
       Destroy(gameObject);
    }
}
