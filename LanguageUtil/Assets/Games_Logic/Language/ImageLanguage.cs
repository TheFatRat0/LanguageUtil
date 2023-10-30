using Language;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.IO;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
#endif

namespace Language
{
    [AddComponentMenu("GameKit/UI/ImageLanguage")]
    public class ImageLanguage : Image
    {
        public string strNameAtlas = "languageAtlas_1";
        [SerializeField]
        public List<string> listValue = new List<string>(Enum.GetNames(typeof(LanguageDefine)).Length);
        [SerializeField]
        public List<string> listGUIDValue = new List<string>(Enum.GetNames(typeof(LanguageDefine)).Length);

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
            SpriteAtlas sprAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/Resources_AssetBundle/Language_" + LanguageManager.GetLanguage() + "/Atlas/" + strNameAtlas + ".spriteatlas");
            sprite = sprAtlas.GetSprite(Path.GetFileNameWithoutExtension((string)(object)value));
            SetNativeSize();
        }

        public void SetValueByLanguage<T>(int languageCodeIndex, T guidValue)
        {
            for (int i = listValue.Count; i <= languageCodeIndex; i++)
            {
                listValue.Add("");
                listGUIDValue.Add("");
            }
            listGUIDValue[languageCodeIndex] = (string)(object)guidValue;
            listValue[languageCodeIndex] = AssetDatabase.GUIDToAssetPath(listGUIDValue[languageCodeIndex]);
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
    }
}