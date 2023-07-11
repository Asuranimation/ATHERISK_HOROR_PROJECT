using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "new Dialogue" , menuName = "SO/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField]  DialogNode[] nodes;
}
