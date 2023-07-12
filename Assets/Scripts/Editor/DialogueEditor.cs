using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DialogueEditor : EditorWindow
    {
        [MenuItem("Window/DialogueEditor")]
        public static void ShowEditorWindow()
        {
            Debug.Log("anda mengaktifkan dialogueEditor");
            GetWindow(typeof(DialogueEditor),false,"Asura Dialog Editor");
        }

        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsetDialogue(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                Debug.Log("openWindowDialogue");
                return true;
            }

            return false;
        }
    }
}
