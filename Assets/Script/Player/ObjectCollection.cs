using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollection : MonoBehaviour
{
    public int piecesToCollected = 3;
    private int piecesCollected = 0;
    [SerializeField] GameManager _gm;

    // Start is called before the first frame update
    void Start()
    {
        _gm.UpdatePiecesText(piecesCollected, piecesToCollected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPieces(int _pieces) => piecesCollected = _pieces;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piezas"))
        {
            
            piecesCollected++;
            _gm.UpdatePiecesText(piecesCollected, piecesToCollected);
            Destroy(collision.gameObject);
            if(piecesCollected >= piecesToCollected)
            {
                Debug.Log("Se abrio la puerta");
            }
        }
    }
}
