using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Language;
using System;

public class GameObjectLanguageController : MonoBehaviour, LanguageComponentInterface
{
    [SerializeField]
    public List<bool> listValue = new List<bool>();

    void OnEnable()
    {
        SetLanguage<bool>();
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
        gameObject.SetActive((bool)(object)value);
    }

    public void SetValueByLanguage<T>(int languageCodeIndex, T value)
    {
        for(int i = listValue.Count; i <= languageCodeIndex;i++)
        {
            listValue.Add(false);
        }
        listValue[languageCodeIndex] = (bool)(object)value;
    }

    public T GetValueByLanguage<T>(int languageCodeIndex)
    {
        if(listValue.Count<=languageCodeIndex)
        {
            return (T)(object)false;
        }
        return (T)(object)listValue[languageCodeIndex];
    }
}
