using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [System.Serializable]
    public class DialogNode
    {
        public string uniqueID;
        public string text;
        public string[] children;
        public Rect positionNode = new Rect(0,0,300,160);
    }
    
}

