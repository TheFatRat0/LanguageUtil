using Language;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using UnityEngine;
using System;

namespace LanguageEditor
{
    [CustomEditor(typeof(ImageLanguage), true)]
    [CanEditMultipleObjects]
    public class ImageLanguageInspector : ImageEditor
    {
        private ImageLanguage m_ui;
        SerializedProperty m_txtAtlasName;
        GUIContent m_TexAtlasNameContent;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_ui = (ImageLanguage)target;
            m_TexAtlasNameContent = EditorGUIUtility.TrTextContent("AtlasName");
            m_txtAtlasName = serializedObject.FindProperty("strNameAtlas");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            foreach (LanguageDefine code in Enum.GetValues(typeof(LanguageDefine)))
            {
                m_ui.SetValueByLanguage(code.GetHashCode(), OnInspectorLanguage(code.ToString(), m_ui.GetGUIDValueByLanguage<string>(code.GetHashCode())));
            }
            EditorGUILayout.PropertyField(m_txtAtlasName, m_TexAtlasNameContent);
            serializedObject.ApplyModifiedProperties();
        }

        string OnInspectorLanguage(string strLanguageName, string value)
        {
            Sprite spr = null;
            if (!string.IsNullOrEmpty(value))
            {
                spr = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(value));
            }
            Sprite sprOld = spr;
            //路径赋值
            spr = (Sprite)EditorGUILayout.ObjectField(strLanguageName, spr, typeof(Sprite),true);
            if (sprOld != spr)
            {
                EditorUtility.SetDirty(m_ui);
                //自动移动位置
                LanguageStaticInspector.ReplaceSpriteToFolder(spr, strLanguageName, int.Parse(m_ui.strNameAtlas.Replace("languageAtlas_", "")));
            }
            return AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(spr));
        }
    }   
}