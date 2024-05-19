using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<ListaDeMovimento> listaDeMovimentos = new List<ListaDeMovimento>();
    public int currentIndex;
    public bool canOpen=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen)
        {
            if (currentIndex < listaDeMovimentos.Count)
            {
                if (Input.GetKeyDown(listaDeMovimentos[currentIndex].movementKey))
                {
                    Debug.Log("Executed: " + listaDeMovimentos[currentIndex].movementName);
                    currentIndex++;
                }
                else
                {
                    foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(key) && key != listaDeMovimentos[currentIndex].movementKey)
                        {
                            Debug.Log("Incorrect key pressed. Restarting sequence.");
                            currentIndex = 0;
                            break;
                        }
                    }
                }
            }
        }
        if (currentIndex >= listaDeMovimentos.Count)
        {
            Debug.Log("Abro la puerta");
        }
    }

}
