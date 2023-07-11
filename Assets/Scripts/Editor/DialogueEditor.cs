using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DialogueEditor : EditorWindow
    {
        [MenuItem("Window/DialogueEditor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor),false,"Asura Dialogue Editor");
        }
    }
    
}
