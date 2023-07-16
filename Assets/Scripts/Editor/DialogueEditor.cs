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
                    /* jadikan new dialog ini ref dari EditorGUILayout.TextField(node.text); berarti nilai string mereka sama
                     disini jika ada perubahan text maka jelas newDialogue tidak sama dengan node.text , jadi yg ada perubahan 
                     string aja yg di simpan dalam setDirty, kalo ga gini ya semua disimpan dalam set dirty meski itu perubahan
                     yg tidak diperlukan, beginilah cara atasinya */
                    string newDialogue = EditorGUILayout.TextField(node.text);
                    if (newDialogue != node.text)
                    {
                        node.text = newDialogue;
                        EditorUtility.SetDirty(selectedDialogue);
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
