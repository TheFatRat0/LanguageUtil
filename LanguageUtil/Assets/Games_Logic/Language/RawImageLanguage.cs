using Language;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using System.IO;
using UnityEditor.Experimental.SceneManagement;
#endif

namespace Language
{
    [AddComponentMenu("GameKit/UI/RawImageLanguage")]
    public class RawImageLanguage : RawImage
    {
        [SerializeField]
        public List<string> listValue = new List<string>(Enum.GetNames(typeof(LanguageDefine)).Length);
        [SerializeField]
        public List<string> listGUIDValue = new List<string>(Enum.GetNames(typeof(LanguageDefine)).Length);
        [SerializeField]
        public List<Vector2> listSizeValue = new List<Vector2>(Enum.GetNames(typeof(LanguageDefine)).Length);

        protected override void Awake()
        {
            SetLanguage<string>();
            base.Awake();
        }

        public void SetLanguage<T>()
        {
            if (!Application.isPlaying)
                return;
            T value = GetValueByLanguage<T>(LanguageManager.GetLanguage().GetHashCode());
            if (value == null)
            {
                return;
            }
            SetLanguageValue<T>(value);
        }

        public void SetLanguageValue<T>(T value)
        {
            texture = AssetDatabase.LoadAssetAtPath<Texture2D>((string)(object)value);
            GetComponent<RectTransform>().sizeDelta = listSizeValue[LanguageManager.GetLanguage().GetHashCode()];
            SetNativeSize();
        }

        public void SetValueByLanguage<T>(int languageCodeIndex, T guidValue)
        {
            for (int i = listValue.Count; i <= languageCodeIndex; i++)
            {
                listValue.Add("");
                listGUIDValue.Add("");
                listSizeValue.Add(new Vector2(0, 0));
            }
            listGUIDValue[languageCodeIndex] = (string)(object)guidValue;
            listValue[languageCodeIndex] = AssetDatabase.GUIDToAssetPath(listGUIDValue[languageCodeIndex]);
            listSizeValue[languageCodeIndex] = GetTextureSize(listValue[languageCodeIndex]);
        }

        public T GetValueByLanguage<T>(int languageCodeIndex)
        {
            if (listValue.Count <= languageCodeIndex || listValue[languageCodeIndex] == null)
            {
                return (T)(object)"";
            }
            return (T)(object)listValue[languageCodeIndex];
        }

        public T GetGUIDValueByLanguage<T>(int languageCodeIndex)
        {
            if (listGUIDValue.Count <= languageCodeIndex || listGUIDValue[languageCodeIndex] == null)
            {
                return (T)(object)"";
            }
            return (T)(object)listGUIDValue[languageCodeIndex];
        }

        public static Vector2 GetTextureSize(string filePath)
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(filePath) as TextureImporter;
            int width = 0;
            int height = 0;
            if (textureImporter == null)
            {
                width = 0;
                height = 0;
            }
            else
            {
                object[] args = new object[2] { 0, 0 };
                MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
                mi.Invoke(textureImporter, args);
                width = (int)args[0];
                height = (int)args[1];
            }
            return new Vector2(width, height);
        }
    }
}