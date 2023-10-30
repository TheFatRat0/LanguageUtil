using Language;
using UnityEditor;
using System;
namespace LanguageEditor
{
    [CustomEditor(typeof(TextLanguage), true)]
    [CanEditMultipleObjects]
    public class TextLanguageInspector : UnityEditor.UI.TextEditor
    {
        private TextLanguage m_ui;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_ui = (TextLanguage)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            foreach(LanguageDefine code in Enum.GetValues(typeof(LanguageDefine)))
            {
                EditorGUILayout.LabelField(code.ToString());
                m_ui.SetValueByLanguage(code.GetHashCode(), OnInspectorLanguage(code.ToString(), m_ui.GetValueByLanguage<string>(code.GetHashCode())));
            }
            serializedObject.ApplyModifiedProperties();
        }

        string OnInspectorLanguage(string strLanguageName, string value)
        {
            string result = EditorGUILayout.TextArea(value);
            if(result != value)
            {
                EditorUtility.SetDirty(m_ui);
            }
            return result;
        }
    }   
}