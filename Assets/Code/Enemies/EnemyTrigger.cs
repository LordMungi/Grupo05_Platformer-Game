using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private Enemy[] EnemiesToTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerLayer) != 0)
        {
            foreach (Enemy e in EnemiesToTrigger)
            {
                e.Activate();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerLayer) != 0)
        {
            foreach (Enemy e in EnemiesToTrigger)
            {
                e.Deactivate();
            }
        }
    }
}
