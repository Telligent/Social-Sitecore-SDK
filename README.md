# Sitecore SDK for Zimbra Social
##Sitecore Social SDK
>###***BETA Disclaimer
This SDK is currently a work in progress and currently in active development.  This means it is subject to change at any time and is not currently appropriate for production use.  The beta version is appropriate for testing and initial development with that understanding.

####Depedencies
The Sitecore SDK requires that you have installed and are running Zimbra Social and Sitecore.  As Such this SDK has dependencies on libraries that for licensing reasons are not made available as part of this SDK.  You will need to obtain these libraries from their resepctive installations and copy them to the */lib* folder before compliling.  They are listed below.

**Sitecore**
- Sitecore.Client.dll
- Sitecore.Kernel.dll

**Zimbra Social**



###What is the SDK?
The SDK is a developer platform/framework to allow you to to interact with Zimbra Social content from inside Sitecore.

####Is this the same as Zimbra Connect for Sitecore?
No and they are not compatible to be run together.  While the technology behind the connector and this SDK are the same, the SDK is meant to provide lower level access to the remoting framework than the connector.  The connector is best described as a full product implementation on the underlying remoting framework.  The SDK is built on that framework but does not use widget rendering and exposes API access the to the remoting framework whereas the connector did not.  This made custom work very challenging in the connector whereas the SDK is developer focused.

####What makes this different from using the Zimbra Social REST API?
REST is a great way to integrate with any third party site becasue it is technology independent and becasue of its use of standard XML and JSON for data transfer.  It is still a viable integration.  The SDK is more of a remoting infrastructure.  It still relies on HTTP to interact with Zimbra social, however it is remotely invoking the Social Widget APIs versus the REST APIs and instead of XML and JSON, you receive dynamic objects that allow you to interact with the data much like you would the strongly types classes inside Zimbra Social.  For that reason it is does require the same version of the .NET framework as your Zimbra Social site.   In addition the SDK handles authentication via Oauth for you meaning with some simple configuration you do not have to implement the Oauth flow yourself.

####Whats the difference between this and Remote Studio Widgets?
I am so glad you asked! Nothing.  This SDK is an implementation of Remote Studio Widgets.  It just so happens we did some of the busy work for you surrounding user identification, authentication and configuration.  These are the most challenging parts of an RSW implementation.

####What about performance?
You still have to consider that the SDK is dependant on making HTTP requests to Zimbra Social so any hinderance in that communication can cause performance issues, same as a REST Api implementation.  You also need to consider how many requests are being made.  With REST this is easy to manage becasue you have to make the call yourself.  In the SDK the benefit of not having to do this can become a downside as well since it will make the calls it needs depending on the call.  In general for every one call to Host.ExecuteMethod() or Host.GetProperty() will result in at least 1 HTTP request, however as you interact with the returned dyanamic objects subsequent requests can be made under the covers.  The wiki will outline some of these considerations and also help youy choose if its truly appropriate to use the SDK in some scenarios.

####Where is the documentation?
Please refer to the Wiki section of this repository

####How do I report a bug?
You can use the issues section of this repository
