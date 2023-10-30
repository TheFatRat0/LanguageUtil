using Language;
using UnityEditor;
using System;
using UnityEngine;

namespace LanguageEditor
{
    [CustomEditor(typeof(GameObjectLanguageController), true)]
    [CanEditMultipleObjects]
    public class GameObjectLanguageControllerInspector : Editor
    {
        private GameObjectLanguageController m_ui;

        void OnEnable()
        {
            m_ui = (GameObjectLanguageController)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            foreach (LanguageDefine code in Enum.GetValues(typeof(LanguageDefine)))
            {
                m_ui.SetValueByLanguage(code.GetHashCode(), OnInspectorLanguage(code.ToString(), m_ui.GetValueByLanguage<bool>(code.GetHashCode())));
            }
            serializedObject.ApplyModifiedProperties();
        }

        bool OnInspectorLanguage(string strLanguageName, bool value)
        {
            bool result = EditorGUILayout.Toggle(strLanguageName,value);
            if (result != value)
            {
                EditorUtility.SetDirty(m_ui);
            }
            return result;
        }
    }   
}