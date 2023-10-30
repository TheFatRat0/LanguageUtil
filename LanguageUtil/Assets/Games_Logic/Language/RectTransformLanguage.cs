using Language;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.IO;
using UnityEditor.Experimental.SceneManagement;
#endif

namespace Language
{
    [AddComponentMenu("GameKit/UI/RectTransformLanguage")]
    public class RectTransformLanguage: MonoBehaviour, LanguageComponentInterface
    {
        [SerializeField]
        public List<RectTransform> listValue = new List<RectTransform>(Enum.GetNames(typeof(LanguageDefine)).Length);

        void Awake()
        {
            SetLanguage<RectTransform>();
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
            RectTransform tranSelf = transform.GetComponent<RectTransform>();
            if(tranSelf)
            {
                RectTransform result = (RectTransform)(object)value;
                tranSelf.sizeDelta = result.sizeDelta;
                tranSelf.localScale = result.localScale;
                tranSelf.localPosition = result.localPosition;
                tranSelf.localRotation = result.localRotation;
            }
        }

        public void SetValueByLanguage<T>(int languageCodeIndex, T value)
        {
            for (int i = listValue.Count; i <= languageCodeIndex; i++)
            {
                listValue.Add(null);
            }
            listValue[languageCodeIndex] = (RectTransform)(object)value;
        }

        public T GetValueByLanguage<T>(int languageCodeIndex)
        {
            if (listValue.Count <= languageCodeIndex)
            {
                return (T)(object)null;
            }
            return (T)(object)listValue[languageCodeIndex];
        }
    }
}