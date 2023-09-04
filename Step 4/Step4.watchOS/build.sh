#!/bin/sh

#xcodebuild -list -project Step4.watchOS.xcodeproj
xcodebuild -project Step4.watchOS.xcodeproj -scheme "Step4.watchOS WatchKit App" build
