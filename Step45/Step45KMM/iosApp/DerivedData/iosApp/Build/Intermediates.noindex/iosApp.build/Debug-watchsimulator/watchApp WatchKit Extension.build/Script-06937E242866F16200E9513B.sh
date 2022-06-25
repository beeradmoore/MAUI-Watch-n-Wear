#!/bin/sh
# Type a script or drag a script file from your workspace to insert its path.
cd "$SRCROOT/.."
./gradlew :shared:embedAndSignAppleFrameworkForXcode

