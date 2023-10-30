using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using System.IO;
using UnityEditor.Experimental.SceneManagement;
#endif

namespace Language
{
    [AddComponentMenu("GameKit/UI/TextLanguage")]
    public class TextLanguage : Text, LanguageComponentInterface
    {
        [SerializeField]
        public List<string> listValue = new List<string>(Enum.GetNames(typeof(LanguageDefine)).Length);

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
            text = (string)(object)value;
        }

        public void SetValueByLanguage<T>(int languageCodeIndex, T value)
        {
            for (int i = listValue.Count; i <= languageCodeIndex; i++)
            {
                listValue.Add("");
            }
            listValue[languageCodeIndex] = (string)(object)value;
        }

        public T GetValueByLanguage<T>(int languageCodeIndex)
        {
            if (listValue.Count <= languageCodeIndex || listValue[languageCodeIndex] == null)
            {
                return (T)(object)"";
            }
            return (T)(object)listValue[languageCodeIndex];
        }
    }
}