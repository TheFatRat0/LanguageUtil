# LanguageUtil
用于unity的本地化/多语言翻译工具

## 工具功能

解决不同语言情景下，text、image、rawimage的属性变化

![image-20231030221257451](READMEImage\image-20231030221257451.png)

![image-20231030221245477](READMEImage\image-20231030221245477.png)

解决不同语言情景下，gameObject的是否显示、rectTransform的属性变化

![image-20231030221416708](READMEImage\image-20231030221416708.png)

![image-20231030221430335](READMEImage\image-20231030221430335.png)

text-textLanguage、image-imageLanguage、rawimage-rawimageLanguage的无损转换

![image-20231030220850648](READMEImage\image-20231030220850648.png)

![image-20231030220911885](READMEImage\image-20231030220911885.png)

符合AssetBundle分语言包打包的方式，组件采用弱引用的方式，不会将资源打进同一个moudle包中

![image-20231030221453514](READMEImage\image-20231030221453514.png)

## TODO

将代码中固定的path改成配置文件供大家自由配置

bugfix