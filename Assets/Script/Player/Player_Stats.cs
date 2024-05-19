using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerStats", menuName ="Player/Stats")]
public class Player_Stats : ScriptableObject
{
    [Header("Life Stats")]
    public int _lifePoints;

    [Header("Move Stats")]
    public float _speed;
    public float _jumpForce;
    public LayerMask _layer;
 
}
