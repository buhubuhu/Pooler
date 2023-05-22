using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace BuhuBuhu.Pooler.Editor
{
    public abstract class BasePoolDatabaseEditor<T> : UnityEditor.Editor where T : IBaseType
    {
        private SerializedProperty _networkObjectsProp;
        private ReorderableList _reorderableList;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            Rect listRect = GUILayoutUtility.GetLastRect();
            listRect.y += _reorderableList.headerHeight;
        }

        private void OnEnable()
        {
            _networkObjectsProp = serializedObject.FindProperty("_pools");
            _reorderableList = new ReorderableList(serializedObject, _networkObjectsProp, true, true, true, true)
            {
                drawHeaderCallback = DrawHeader,
                drawElementCallback = DrawElement,
                elementHeightCallback = GetElementHeight
            };
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, $"{typeof(T).Name}'s Database", EditorStyles.boldLabel);
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (_networkObjectsProp.arraySize == 0)
                return;
            var element = _networkObjectsProp.GetArrayElementAtIndex(index);
            var tagProp = element.FindPropertyRelative("Tag");
            var nameProp = tagProp.FindPropertyRelative("_name");
            var prefabProp = element.FindPropertyRelative("Prefab");
            var limitMaxInstancesProp = element.FindPropertyRelative("LimitMaxInstances");
            var maxInstancesProp = element.FindPropertyRelative("MaxInstances");

            rect.y += 2;
            rect.height -= 4;

            EditorGUI.indentLevel++;

            // Get the position for the foldout and its label
            var foldoutPosition = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            var labelPosition = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            var style = new GUIStyle(GUIStyle.none);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 14;
            var foldoutlabel = nameProp.stringValue;
            if (prefabProp.objectReferenceValue == null)
            {
                foldoutlabel += " (Prefab missing)";
                style.normal.textColor = Color.red;
            }
            else
            {
                style.normal.textColor = Color.white;
            }
            element.isExpanded = EditorGUI.Foldout(foldoutPosition, element.isExpanded, new GUIContent(foldoutlabel), true, style);
            labelPosition.y += 2;

            if (element.isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight), rect.width, EditorGUIUtility.singleLineHeight), tagProp);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + 2) * 2, rect.width, EditorGUIUtility.singleLineHeight), prefabProp);

                EditorGUI.PropertyField(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + 2) * 3, rect.width, EditorGUIUtility.singleLineHeight), limitMaxInstancesProp);

                if (limitMaxInstancesProp.boolValue)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight + 2) * 4, rect.width, EditorGUIUtility.singleLineHeight), maxInstancesProp);
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel = 0;
        }

        private float GetElementHeight(int index)
        {
            if (_networkObjectsProp.arraySize == 0)
                return 0;
            var element = _networkObjectsProp.GetArrayElementAtIndex(index);
            var limitMaxInstancesProp = element.FindPropertyRelative("LimitMaxInstances");
            if (element.isExpanded)
            {
                if (limitMaxInstancesProp.boolValue)
                {
                    return 105;
                }
                else
                {
                    return 80;
                }
            }
            else
            {
                return 25;
            }
        }
    }
}








