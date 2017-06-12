# DummyClock UWP

A smart clock implementation for Raspberry Pi 3 + Windows 10 IoT

## Intro

Basically it just an Windows 10 UWP app to keep displaying information, including:

- RMIT Timetable, based on my [RMITer](https://github.com/huming2207/Rmiter) project
- PTV public transport timetable, based on [Ptv.Net](https://github.com/huming2207/Ptv.Net) project
- Yahoo weather, based on my [YahooWeather.Net](https://github.com/huming2207/YahooWeatherDotNet) project

Let's have a look for some pics.

This is the app running on my laptop when debugging.

![](https://raw.githubusercontent.com/huming2207/DummyClock/master/Pics/IMG_5459.jpg)

This is what it looks like after it has been deployed and running on the Raspberry Pi.

![](https://raw.githubusercontent.com/huming2207/DummyClock/master/Pics/IMG_5474.jpg)

## How to build

Basically you just need:

- A Raspberry Pi 3 with 8GB+ microSD card, of course
- Windows 10 (version 14393+) installed on your PC and Raspberry Pi
- Visual Studio 2017, 15.1+
- Windows 10 SDK 10586 to 14393
- Your own RMIT student ID and PTV API Key [filled in here](https://github.com/huming2207/DummyClock/blob/master/DummyClock/Settings.example.cs)