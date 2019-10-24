INPUT MANAGER README
====================

Please, consult news, videos and an updated documentation in the package's web: http://gargore.com/uas/input-manager/
An online manual is available at: http://gargore.com/uas/input-manager/manual


Quick start guide
=================

Althought you can find a complete step by step guide in the manual, here is a brief description on how to start.

The purpose of this package is to allow you to develop easily device interfaces that work with Extensible Input Manager.

If you need a Input library for production purporses, it is better that you get the bundle pc or bundle handheld from the asset store because they have optimized code and additional features.


Registering interfaces
======================

Before using your interfaces, you must register them. On this version you cannot use the GInputManager table, so the only way to register your extensions is by script.

Call GInput.registerInterface using the desired axisname as first parameter and a instance of your interface object as second parameter.

You can call after that to GInput.configureInterface to pass a configuration string.

There is an example on Plugins/Extensible Input Manager/Examples/Scripts


Creating new interfaces
=======================

The power of this extension is to allow you to use your own or third party interfaces.

For creating a new interface, you must implement GInputInterface. There is a dummy implementation (GInputInterfaceBase) which will ease you in the process of creating new interfaces simplifying the code to only the elements that you really want to implement.

You have an example in the Plugins/Extensible Input Manager/Scripts/Interfaces folder: GInputInterfaceExample of how to implement axes and buttons.

The steps for implementing a feature from GInputInterface is to rewrite a implementsFeature function and return true (or the number of seconds of the interval).

Then rewrite the feature function itself. You will be passed the button or axis name as first parameter.

For the InputConfig implementation you will be passed as second parameter the new configuration string or null if the library wants only that you return the configuration. You must return a string with the accepted configuration.


See the complete manual for an extended reference of these and other options.
