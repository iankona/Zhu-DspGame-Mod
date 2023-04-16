<div align="center" style="font-size: 42px; font-weight:bold;">戴森球计划Mod</div>
---



<div align="left" style="font-size: 32px;font-weight:bold;">一、戴森球计划Mod
    <a style="font-size: 24px;">What is this Script? </a></div> 
<div align="right" style="font-size: 18px;font-weight:bold;">日期：
    <a style="font-size: 18px;">2023年3月21日 </a></div> 

　　项目代码主要参考了[Windows10CE](https://github.com/Windows10CE/DSPPlugins)’s  [DSPPlugins](https://github.com/Windows10CE/DSPPlugins)项目、[mattsemar](https://github.com/mattsemar)’s  [dsp-long-arm](https://github.com/mattsemar/dsp-long-arm)项目、[hetima](https://github.com/hetima)'s [DSP_ExpandTouchableRange](https://github.com/hetima/DSP_ExpandTouchableRange)项目，和[是小庄庄鸭](https://space.bilibili.com/26024327)’s  戴森球计划Mod制作教学教程（[[戴森球计划Mod制作教学]1.手把手安装需要用到的软件](https://www.bilibili.com/video/BV1pK4y1n7FF)，[[戴森球计划Mod制作教学]2.新建项目及HelloWrold](https://www.bilibili.com/video/BV1UA411T7Jv)，[[戴森球计划Mod制作教学]1.评论区答疑](https://www.bilibili.com/video/BV1dy4y1a747)，[[戴森球计划Mod制作教学]3.代码分析及Mod制作思路](https://www.bilibili.com/video/BV1Gt4y1z7JS)，[[戴森球计划Mod制作教学]采矿机倍率修改(采矿更快)](https://www.bilibili.com/video/BV1At4y1z7vp)）



　　每个dll都是1个单独的功能，主要是6个dll：

- ZHUInstantBuild.dll

  ​	背包有物品时，会消耗物品立即建造，没物品的时候也会立即建造。小飞机起飞后，由于已经建造完会转向飞回背包，会少量耗电。

- ZHUFastMiningMachine.dll

  ​	采矿机2倍采矿速率，l个矿点对应60个/min。大矿机则受影响默认设置为2倍速率采矿，耗电大增。

- ZHU点击拖动建造范围.dll

  ​	非蓝图模式下的连续建造，去除一次只能建设16个建筑，太阳能板现在一次可以连续建设50个以上

- ZHU机甲无人机建造区域.dll

  ​	修改无人机的建设范围，现在全球都可以建设，越远耗电越大，初期使用，机甲会动弹不了，电量都被小飞机用关了。

- ZHU继承物品.dll

  ​	点击电磁学，点击立即研究研究立刻完成，解锁部分蓝糖红糖科技和少量黄糖科技并获得部分后期物品，使用后会被系统判断为游戏异常作弊，但不会影响继续玩游戏。

  ​	解锁科技：

  ​		机舱容量1-3级、机甲核心1-2级、机械骨骼1级、通讯控制1-2级、能量回路1-2级、蓝图1-2级、无人机引擎1级、驱动引擎1-2级、宇宙探索1-2级、电磁学、基础物流系统、自动化冶金、冶炼提纯、基础制造工艺制造台Ⅰ、高级制造工艺制造台Ⅱ、太阳能收集、等离子萃取精炼、基础化工 、应用型超导体

  ​	获得物品：

  ​		电力感应塔50、无线输电塔5、极速传送带1500、极速分拣器600、小型储物仓25、四向分流器25、物流配送器25、配送运输机250、采矿机10、电弧熔炉100、制造台Mk.II 50、行星内物流运输站20、物流运输机600、星际物流运输站5、轨道采集器80、星际物流运输船50、风力涡轮机35、大型采矿机25、反物质燃料棒100

- ZHU取物范围.dll

  ​	可以操作全球设备，打开全球储物箱，不会遇到建筑太远，无法操作的提示。



<div align="left" style="font-size: 32px;font-weight:bold;">二、参考
    <a style="font-size: 24px;">Reference</a></div> 

​		InstantBuild：    [Windows10CE](https://github.com/Windows10CE/DSPPlugins)’s  [DSPPlugins](https://github.com/Windows10CE/DSPPlugins)项目

​		FastMiningMachine：    [是小庄庄鸭](https://space.bilibili.com/26024327)’s  戴森球计划Mod制作教学教程（[[戴森球计划Mod制作教学]1.手把手安装需要用到的软件](https://www.bilibili.com/video/BV1pK4y1n7FF)，[[戴森球计划Mod制作教学]2.新建项目及HelloWrold](https://www.bilibili.com/video/BV1UA411T7Jv)，[[戴森球计划Mod制作教学]1.评论区答疑](https://www.bilibili.com/video/BV1dy4y1a747)，[[戴森球计划Mod制作教学]3.代码分析及Mod制作思路](https://www.bilibili.com/video/BV1Gt4y1z7JS)，[[戴森球计划Mod制作教学]采矿机倍率修改(采矿更快)](https://www.bilibili.com/video/BV1At4y1z7vp)）

​		点击拖动建造范围：    [Windows10CE](https://github.com/Windows10CE/DSPPlugins)’s  [DSPPlugins](https://github.com/Windows10CE/DSPPlugins)项目

​		机甲无人机建造区域：    [mattsemar](https://github.com/mattsemar)’s  [dsp-long-arm](https://github.com/mattsemar/dsp-long-arm)项目

​		继承物品：    [Windows10CE](https://github.com/Windows10CE/DSPPlugins)’s  [DSPPlugins](https://github.com/Windows10CE/DSPPlugins)项目

​		取物范围：    [hetima](https://github.com/hetima)'s [DSP_ExpandTouchableRange](https://github.com/hetima/DSP_ExpandTouchableRange)项目



