using Language;
using UnityEditor;
using System;
using UnityEngine;

namespace LanguageEditor
{
    [CustomEditor(typeof(RectTransformLanguage), true)]
    [CanEditMultipleObjects]
    public class RectTransformLanguageInspector:Editor
    {
        private RectTransformLanguage m_ui;

        void OnEnable()
        {
            m_ui = (RectTransformLanguage)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            foreach (LanguageDefine code in Enum.GetValues(typeof(LanguageDefine)))
            {
                m_ui.SetValueByLanguage(code.GetHashCode(), OnInspectorLanguage(code.ToString(), m_ui.GetValueByLanguage<RectTransform>(code.GetHashCode())));
            }
            serializedObject.ApplyModifiedProperties();
        }

        RectTransform OnInspectorLanguage(string strLanguageName, RectTransform value)
        {
            RectTransform result = (RectTransform)EditorGUILayout.ObjectField(strLanguageName,value, typeof(RectTransform),true);
            if (result != value)
            {
                EditorUtility.SetDirty(m_ui);
            }
            return result;
        }
    }   
}