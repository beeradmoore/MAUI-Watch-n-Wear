# MAUI Watch 'n Wear
This repository dedicated to exploring adding watchOS and Wear OS applications in MAUI iOS and Android builds. 

To accomplish this we will attempt various different steps to slowly add more and more functionality. A rough outline of how this will go is:

### Basic app concept
The app is intended to be simple and demonstrate basic functionality and communication between a phone app and a wearable app. In both cases the wearable app will be able to function in a standalone mode. It will display a number on the screen with -1/+1 buttons in order to increment the number. The wearable component will do the same, but both should be synced together. If you increase the number on the app it should increase it in the watch. If you increase the number on the watch it should increase it in the app.

If the app is considered a "score keeper" type app it may also possible to distribute to the App Store and Google Play for live testing.

### Step 1
Create a Xamarin.iOS and Xamarin.Android project with the accompanying wearable app with the functionality as above.

### Step 1a (optional)
Test the functionality of an iPhone with a paired Wear OS device. This appears to be possible, see the Android developer docs on independent vs standalone Wear OS app below.

### Step 2
Create a MAUI application and add relevant tooling to bundle the Xamarin wearable app from step 1.

### Step 3
Because Xamarin will reach end of life at some point ([2 years after final release](https://dotnet.microsoft.com/en-us/platform/support/policy/xamarin)) also test building of a native Obj-C/Swift watchOS app and a native Java/Kotlin Wear OS app bundlded within the MAUI project created in step 2.





#### Notes and links
[Xamarin Android wear docs on manual packaging of apps](https://docs.microsoft.com/en-us/xamarin/android/wear/deploy-test/packaging?tabs=windows#manual-packaging)

[Bundling native watchOS app in a Xamarin.iOS project](https://github.com/xamarin/xamarin-macios/issues/10070)

[Android developer docs on independent vs standalone Wear OS apps](https://developer.android.com/training/wearables/apps/standalone-apps)

