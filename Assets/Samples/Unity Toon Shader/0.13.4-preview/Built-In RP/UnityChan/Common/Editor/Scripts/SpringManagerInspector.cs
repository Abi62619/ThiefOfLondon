using UnityEditor;
using UnityEngine;

namespace UnityChan.Editor
{
    [CustomEditor(typeof(SpringManager))]
    public class SpringManagerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SpringManager manager = (SpringManager)target;

            if (GUILayout.Button("Init From Children"))
            {
                SpringBone[] springBones = manager.GetComponentsInChildren<SpringBone>(true);

                // FIX: use string instead of nameof + updated field name
                SerializedProperty springBonesProp = serializedObject.FindProperty("springBones");

                if (springBonesProp == null)
                {
                    Debug.LogError("Could not find 'springBones' field on SpringManager. Check the actual variable name.");
                    return;
                }

                springBonesProp.arraySize = springBones.Length;

                for (int i = 0; i < springBones.Length; i++)
                {
                    springBonesProp.GetArrayElementAtIndex(i).objectReferenceValue = springBones[i];
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}