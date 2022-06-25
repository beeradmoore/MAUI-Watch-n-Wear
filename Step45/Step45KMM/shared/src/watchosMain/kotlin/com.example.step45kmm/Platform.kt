package com.example.step45kmm

import platform.WatchKit.WKInterfaceDevice

actual class Platform actual constructor() {
    actual val platform: String = WKInterfaceDevice.currentDevice().systemVersion
}
