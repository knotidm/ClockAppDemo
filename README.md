# ClockAppDemo

Unity 2021.3.4f1 LTS on Universal Render Pipeline.
Needed to generate a custom keystore for the Android build, if you have problems to build Android .apk with 2021.3.4f1 gradle tools you need to generate your own custom keystore and set it in the PlayerSettings Publish Settings tab.

 1. UI is created with the reference resolution 1080x1920 portrait for mobile and for PC windowed with resizable window. UI is implemented with RectTransform Anchors to fit both PC and mobile devices' aspect ratio and resolutions.
 2. I tried to implement a project based on Simple Clean Architecture, but the problem I encountered was when the Presenter needed to have a UI element reference to have the possibility to take action on this UI element - maybe there is an option to refactor this code. Also nice to have will be to refactor test scripts not to have redundant code - implementing reusable mocks of the objects.
 3. To support VR in this application the canvas needs to be created in the world space and can be used like a popup that can be shown and hidden. This canvas can also follow the player and be rotated toward the player to be visible from different angles. It also needs to add a virtual keyboard to support input fields. Furthermore, this app can implement functionality to pin canvas in the space using spatial anchors to be used like a windowed app in visionOS and other MixedReality solutions.
