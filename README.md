# MAUI Watch 'n Wear
This repository dedicated to exploring adding watchOS and Wear OS applications in MAUI iOS and Android builds. 

### Status 
| Step   | iOS Status | Android Status |
|--------|------------|----------------|
| Step 1 | ✅         | ✅              |
| Step 2 | ✅         | ✅              |
| Step 3 | ❌         | ✅              |
| Step 4 | Untested   | Untested       |
| Step 5 | Untested   | Untested       |




### Basic app concept
The app is intended to be simple and demonstrate basic functionality and communication between a phone app and a wearable app. In both cases the wearable app will be able to function in a standalone mode. It will display a number on the screen with -1/+1 buttons in order to increment the number. The wearable component will do the same, but both should be synced together. If you increase the number on the app it should increase it in the watch. If you increase the number on the watch it should increase it in the app.

If the app is considered a "score keeper" type app it may also possible to distribute to the App Store and Google Play for live testing.

To accomplish this we will attempt various different steps to slowly add more and more functionality. A rough outline of how this will go is:

### Step 1
Create a Xamarin.iOS and Xamarin.Android project with the accompanying wearable app with the functionality as above.

#### Result
Works like a charm as it has for some time.

### Step 1a (optional)
Test the functionality of an iPhone with a paired Wear OS device. This appears to be possible, see the Android developer docs on independent vs standalone Wear OS app below.

#### Result
Not attempted yet.

### Step 2
Create a Xamarin.Forms application and which has included in the wearable projects from step 1.

#### Result
Works like a charm as it has for some time.

### Step 3
Create a MAUI application and add relevant tooling to bundle the Xamarin wearable app from step 1.

#### Result
Thanks to some [online help](https://twitter.com/ivictorhugo/status/1526561045698969604) it is possible to build a net6.0-android app for a wearOS device, which is a different app target type than what was used in step 1 but this is preferred. Unfortunately the MAUI app can't reference the Step3.WatchOSApp project and there is no net6.0-watchos support.

### Step 4
Because Xamarin will reach end of support at some point ([2 years after final release](https://dotnet.microsoft.com/en-us/platform/support/policy/xamarin)) also test building of a native Obj-C/Swift watchOS app and a native Java/Kotlin Wear OS app bundlded within the MAUI project created in step 3.

### Step 5
Implement both wearable apps with [Kotlin Multiplatform Mobile](https://kotlinlang.org/lp/mobile/) to keep a single logic base. 



#### Notes and links
[Xamarin Android wear docs on manual packaging of apps](https://docs.microsoft.com/en-us/xamarin/android/wear/deploy-test/packaging?tabs=windows#manual-packaging)

[Bundling native watchOS app in a Xamarin.iOS project](https://github.com/xamarin/xamarin-macios/issues/10070)

[Android developer docs on independent vs standalone Wear OS apps](https://developer.android.com/training/wearables/apps/standalone-apps)

