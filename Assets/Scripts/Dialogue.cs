using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu (fileName = "new Dialogue" , menuName = "ScriptableObject/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private List<DialogNode> nodes;

#if UNITY_EDITOR
        private void Awake()
        {
            if (nodes.Count == 0)
            {
                nodes.Add(new DialogNode()); 
            }
            else
            {
                nodes.Add(new DialogNode()); 
            }
        }
#endif
        public IEnumerable<DialogNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogNode GetRootNode()
        {
            return nodes[1];
        }
    }
    
    
}

