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
        private GUIStyle nodeSytle;
        private bool dragging = false;
        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            nodeSytle = new GUIStyle();
            string path = "Assets/Sprites/TexDialogue.png"; // Ubah sesuai dengan jalur relatif yang benars
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            nodeSytle.normal.background = texture;
            nodeSytle.padding = new RectOffset(40, 40, 20, 20);
            nodeSytle.border = new RectOffset(20, 20, -20, 30);
        }
        private void OnGUI()
        {
            if (selectedDialogue != null)
            {
                ProcessEvents();
                foreach (DialogNode node in selectedDialogue.GetAllNodes())
                {
                    OnGuiNode(node);
                }
            }
            else
            {
                EditorGUILayout.LabelField("Dialogue not found");
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && !dragging)
            {
                dragging = true;
            }
            else if (Event.current.type == EventType.MouseDrag && dragging)
            {
                Undo.RecordObject(selectedDialogue, "Move Dialogue Node");
                selectedDialogue.GetRootNode().positionNode.position = Event.current.mousePosition;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && dragging)
            {
                dragging = false;
            }
        }
        
        private void OnGuiNode(DialogNode node)
        {
            GUILayout.BeginArea(node.positionNode, nodeSytle);
            EditorGUILayout.LabelField("Node", new GUIStyle(EditorStyles.boldLabel) { normal = new GUIStyleState() { textColor = Color.black } });
            EditorGUI.BeginChangeCheck();
            string newUniqueId = EditorGUILayout.TextField(node.uniqueID);
            string newDialogue = EditorGUILayout.TextField(node.text);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedDialogue, "Update Dialogue Text");
                node.uniqueID = newUniqueId;
                node.text = newDialogue;
            }
            GUILayout.EndArea();
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            selectedDialogue = newDialogue;
            Repaint();
        }
    }
}
