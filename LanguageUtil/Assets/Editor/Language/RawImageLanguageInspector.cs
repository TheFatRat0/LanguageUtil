using Language;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using UnityEngine;
using System;
namespace LanguageEditor
{
    [CustomEditor(typeof(RawImageLanguage), true)]
    [CanEditMultipleObjects]
    public class RawImageLanguageInspector : RawImageEditor
    {
        private RawImageLanguage m_ui;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_ui = (RawImageLanguage)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            foreach (LanguageDefine code in Enum.GetValues(typeof(LanguageDefine)))
            {
                m_ui.SetValueByLanguage(code.GetHashCode(), OnInspectorLanguage(code.ToString(), m_ui.GetGUIDValueByLanguage<string>(code.GetHashCode())));
            }
            serializedObject.ApplyModifiedProperties();
        }

        string OnInspectorLanguage(string strLanguageName, string value)
        {
            Texture texture = null;
            if (!string.IsNullOrEmpty(value))
            {
                texture = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(value));
            }
            Texture texOld = texture;
            //路径赋值
            texture = (Texture)EditorGUILayout.ObjectField(strLanguageName, texture, typeof(Texture),true);
            if (texOld != texture)
            {
                EditorUtility.SetDirty(m_ui);
                //自动移动位置
                LanguageStaticInspector.ReplaceTextureToFolder(texture, strLanguageName);
            }
            return AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(texture));
        }
    }
}