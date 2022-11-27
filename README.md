# Unknown Rabbit Game
## Introduction

A game demo built with Unity 2021.3.0f1 LTS features and my personal Game Framework.

基于 Unity 2021.3.0f1 的游戏demo，旨在体验Unity新版本特性，体验社区中热度较高的package，以及构建个人的，可复用的游戏开发框架。



## Usage

1. Install Unity 2021.3.0f1 
2. Open scene Assets/Scenes/Launcher.unity and enter play mode



## Packages

项目引入的Package一览：

1. Addressable(1.19.19)：资源管理方案
2. Input System(1.3.0)：输入控制
3. Compilation Visualizer(1.8.3)：编译过程可视化



## Directory Roadmap

* Scripts
  * Framework：不依赖Unity的架构设计，以及开发常用设计模式等，独立编译为Framework.dll
  * UnityBasedFramework：基于前者的，依赖Unity提供API的架构拓展与设计，独立编译为UnityBasedFramework.dll
  * UnknownRabbitGame：基于前两者开发的游戏demo项目
