using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hola");
            Controller controller = collision.gameObject.GetComponent<Controller>();
            collision.gameObject.transform.position = controller._positions[controller._indexpos].position;
        }
    }
}
