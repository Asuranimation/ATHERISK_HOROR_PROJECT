using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DialogueEditor : EditorWindow
    {
        [MenuItem("Tools/DialogueEditor")]
        public static void ShowEditorWindow()
        {
            Debug.Log("anda mengaktifkan dialogueEditor");
            GetWindow(typeof(DialogueEditor),false,"Asura Dialog Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsetDialogue(int instanceID)
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

        private Dialogue selectedDialogue = null;

        private void OnGUI()
        {
            if (selectedDialogue != null)
            {
                foreach (DialogNode node in selectedDialogue.GetAllNodes())
                {
                    EditorGUILayout.LabelField("Node");
                    EditorGUI.BeginChangeCheck();
                    string newUniqueId = EditorGUILayout.TextField(node.uniqueID);
                    string newDialogue = EditorGUILayout.TextField(node.text);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(selectedDialogue,"Update Dialogue Text");
                        node.uniqueID = newUniqueId;
                        node.text = newDialogue;
                    }

                }
            }
            else
                EditorGUILayout.LabelField("Dialogue not found");
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            selectedDialogue = newDialogue;
            Repaint();
        }
    }
}
