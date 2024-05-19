using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollection : MonoBehaviour
{
    public int piecesToCollected = 3;
    private int piecesCollected = 0;
    [SerializeField] GameManager _gm;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;
    private int index=0;
    // Start is called before the first frame update
    void Start()
    {
        _gm.UpdatePiecesText(piecesCollected, piecesToCollected);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piezas"))
        {
            
            piecesCollected++;
            _audioSource.PlayOneShot(_audioClip[index]);
            index++;
            if(index >= _audioClip.Length)
            {
                index = 0;
            }
            _gm.UpdatePiecesText(piecesCollected, piecesToCollected);
            Destroy(collision.gameObject);
            if(piecesCollected >= piecesToCollected)
            {
                Debug.Log("Se abrio la puerta");
            }
        }
    }
}
