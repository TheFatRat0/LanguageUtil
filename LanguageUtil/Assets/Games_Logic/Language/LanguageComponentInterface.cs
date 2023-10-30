using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Language
{
    public interface LanguageComponentInterface
    {
        public void SetLanguage<T>();
        public void SetLanguageValue<T>(T value);
        public T GetValueByLanguage<T>(int LanguageCodeIndex);
        public void SetValueByLanguage<T>(int LanguageCodeIndex, T value);
    }
}

