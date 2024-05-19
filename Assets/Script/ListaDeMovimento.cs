using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMovement", menuName = "Movement")]
public class ListaDeMovimento : ScriptableObject
{
    public string movementName;
    public KeyCode movementKey;
}
