<div align="center" style="font-size: 42px; font-weight:bold;">戴森球计划Mod</div>
---

### 一、戴森球计划Mod



​	适配《戴森球计划·黑雾崛起》大版本更新

​	每个dll都是1个单独的功能，尽可能最小化修改。



### 二、单一功能dll



- Zhu星系内快速移动.dll

  ​	在同星系内星球之间快速移动，在不同星系的星球之间快速移动，抵达其他星系的星球需要解锁对应的宇宙探索等级。不支持在星球的全球视图内快速移动；不支持在星团视图内不同星系之间移动（停留在太阳旁边）。

  

- Zhu超级能量枢纽.dll

  ​	能量枢纽，充放电功率315MW，能量10.26GJ。

  

- Zhu超级蓄电池.dll

  ​	蓄电池放置在地上成为建筑时，蓄电池功率315MW，能量10.26GJ。

  

- Zhu轨道采集器立即建造.dll

  ​	需要背包有物品，轨道采集器能立即建造，其他建筑建造不受影响，减少初期轨道采集器建设时间耗时过久，只能等小飞机慢吞吞飞完的痛点。小飞机起飞后，由于已经建造，会转向飞回背包，有少量耗电。

  

- Zhu海洋星黑雾不登录.dll

  ​	黑雾中继器生成时，若登录位置在海洋星，则不生成。黑雾中继器返回母巢停留后，重新搜索登录星球时，会跳过海洋星。

  

- Zhu荒漠星添加石油.dll

  ​	全星系的海洋星删除所有基础矿物，奇珍矿物则不受影响。在全星系的荒漠星添加煤炭和石油矿物。每个荒漠星添加煤矿量在200个矿脉左右，石油则是100/s左右。初始星系若没有荒漠星，会在冻土星添加煤炭和石油。

  

- Zhu机甲数据修改.dll

  ​	默认卸载项目不生成dll。机甲功率200GJ，护盾充能功率100GJ，护盾量1TJ。沙盒模式近距离观察黑雾母巢用，可以不用在手动点击几百次的白糖科技升级。

  

- Zhu继承物品.dll

  ​	开局继承物品。

  ​	点击电磁学，点击立即研究研究立刻完成，解锁部分蓝糖红糖科技和少量黄糖科技并获得部分后期物品，使用后会被系统判断为游戏异常作弊，但不会影响继续玩游戏。

  ​	解锁科技：

  ​	机舱容量1-3级、机甲核心1-2级、机械骨骼1级、通讯控制1-2级、能量回路1-2级、蓝图1-2级、无人机引擎1级、驱动引擎1-2级、宇宙探索1-2级、电磁学、基础物流系统、自动化冶金、冶炼提纯、基础制造工艺制造台Ⅰ、高级制造工艺制造台Ⅱ、太阳能收集、等离子萃取精炼、基础化工 、应用型超导体

  ​	获得物品：

  ​	电力感应塔50、无线输电塔5、极速传送带1500、极速分拣器600、小型储物仓25、四向分流器25、物流配送器25、配送运输机250、采矿机10、电弧熔炉100、制造台Mk.II 50、行星内物流运输站20、物流运输机600、星际物流运输站5、轨道采集器80、星际物流运输船50、风力涡轮机35、大型采矿机25、反物质燃料棒100。

  

- Zhu取物范围.dll

  ​	可以操作全球设备，打开全球储物箱，不会遇到建筑太远，无法操作的提示。

  

- Zhu矿机采矿留根.dll

  ​	当矿簇里的矿脉小于100个时，则跳转到其他矿脉采矿。这样矿脉不会采空而被清除，矿机采矿速度也不会下降。当所有的矿脉都小于100时，矿机不在采矿，但功率不会减少。此时矿机正常耗电，但矿物产出为零。只有采矿科技升级到157级时，矿机才会重新产矿。

  ​	

- Zhu清除黑雾不涂地基.dll

  ​	清除黑雾的行星基地时，游戏本体会将地面涂层地基的金属色。本dll在填坑黑雾地面基地时，不涂色保留星球原本贴图。但建设建筑时被拍平的凹凸起伏地形无法恢复。期待以后游戏更新，能建设跟随地形起伏而有点变化或者凌乱的建筑风格。

  

- Zhu物流配送机起送量.dll

​		当前游戏的物流配送机是1个物品也会起飞送货，dll修改物流配送机起送量为100。



- Zhu背包物品堆叠倍数.dll

​		背包的物品堆叠倍数为10倍。不影响储物箱等物流系统。



- Zhu小矿机采集整个矿簇.dll

  ​	小采矿机覆盖到矿簇里的任何一个矿脉，会自动将整个矿簇里的所有矿脉纳入采集范围。

  ​	代码编制期间，遇到代码正常，数据是public的，通过控制台打印也确认数据也确实修改了，但就是不起作用，后面推测可能是多线程的进程被守护了，导致我这边修改后，又被其他线程改回去了。后面想了很多办法，最终通过统一矿机的矿脉列表长度方式解决的。原来是有多少矿脉，就生成对应的矿脉列表。现在把采矿机的矿脉列表都统一到32个矿脉，一般的矿脉也就28个以内，32长度的矿脉列表也够用了。
  
  

### 三、下载地址



链接：[https://pan.baidu.com/s/1NDybDSOPT7huJbjNGgU4Mg?pwd=h9uy](http://jump2.bdimg.com/safecheck/index?url=rN3wPs8te/pjz8pBqGzzzz3wi8AXlR5g1NgdDpM6gxJfkP6kyorB1Rg+wNm8kIilYL2mR7Y5XB4XvCSmjns1G+zz4xxl+TDriyZg4x6YnepGlz+36AkIIp2tR38RKSFufNUvbL+ed2n+cik8S+eWO3a+JcsYTQHKjNeaAi215GfNP7pvU1Pt1/eTs9xxhJy2hqSLu/+J0Xa4SjHN9tW1KA==)

提取码：h9uy



### 四、参考

​	项目代码主要参考了[Windows10CE](https://github.com/Windows10CE/DSPPlugins)’s  [DSPPlugins](https://github.com/Windows10CE/DSPPlugins)项目、[mattsemar](https://github.com/mattsemar)’s  [dsp-long-arm](https://github.com/mattsemar/dsp-long-arm)项目、[hetima](https://github.com/hetima)'s  [DSP_FastTravelEnabler](https://github.com/hetima/DSP_FastTravelEnabler)项目、[hetima](https://github.com/hetima)'s [DSP_ExpandTouchableRange](https://github.com/hetima/DSP_ExpandTouchableRange)项目，和[是小庄庄鸭](https://space.bilibili.com/26024327)’s  戴森球计划Mod制作教学教程（[[戴森球计划Mod制作教学]1.手把手安装需要用到的软件](https://www.bilibili.com/video/BV1pK4y1n7FF)，[[戴森球计划Mod制作教学]2.新建项目及HelloWrold](https://www.bilibili.com/video/BV1UA411T7Jv)，[[戴森球计划Mod制作教学]1.评论区答疑](https://www.bilibili.com/video/BV1dy4y1a747)，[[戴森球计划Mod制作教学]3.代码分析及Mod制作思路](https://www.bilibili.com/video/BV1Gt4y1z7JS)，[[戴森球计划Mod制作教学]采矿机倍率修改(采矿更快)](https://www.bilibili.com/video/BV1At4y1z7vp)）


​	轨道采集器立即建造： [Windows10CE](https://github.com/Windows10CE/DSPPlugins)’s  [DSPPlugins](https://github.com/Windows10CE/DSPPlugins)项目

​	继承物品：  [Windows10CE](https://github.com/Windows10CE/DSPPlugins)’s  [DSPPlugins](https://github.com/Windows10CE/DSPPlugins)项目

​	取物范围： [hetima](https://github.com/hetima)'s [DSP_ExpandTouchableRange](https://github.com/hetima/DSP_ExpandTouchableRange)项目

​	快速移动：[hetima](https://github.com/hetima)'s  [DSP_FastTravelEnabler](https://github.com/hetima/DSP_FastTravelEnabler)项目

​	小矿机采集整个矿簇： [是小庄庄鸭](https://space.bilibili.com/26024327)’s  戴森球计划Mod制作教学教程（[[戴森球计划Mod制作教学]1.手把手安装需要用到的软件](https://www.bilibili.com/video/BV1pK4y1n7FF)，[[戴森球计划Mod制作教学]2.新建项目及HelloWrold](https://www.bilibili.com/video/BV1UA411T7Jv)，[[戴森球计划Mod制作教学]1.评论区答疑](https://www.bilibili.com/video/BV1dy4y1a747)，[[戴森球计划Mod制作教学]3.代码分析及Mod制作思路](https://www.bilibili.com/video/BV1Gt4y1z7JS)，[[戴森球计划Mod制作教学]采矿机倍率修改(采矿更快)](https://www.bilibili.com/video/BV1At4y1z7vp)）





