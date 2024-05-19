using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{
    [SerializeField] Transform[] targets;
    public int index = 0;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targets[index].position, speed*Time.deltaTime);
        if (Vector3.Distance(transform.position, targets[index].position) < 0.1f)
        {       
            index++;
            if (index >= targets.Length)
            {
                index = 0;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
