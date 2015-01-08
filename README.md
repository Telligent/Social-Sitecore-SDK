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

####What makes this different from using the Zimbra Social REST API?
REST is a great way to integrate with any third party site becasue it is technology independent and becasue of its use of standard XML and JSON for data transfer.  It is still a viable integration.  The SDK is more of a remoting infrastructure.  It still relies on HTTP to interact with Zimbra social, however it is remotely invoking the Social Widget APIs versus the REST APIs and instead of XML and JSON, you receive dynamic objects that allow you to interact with the data much like you would the strongly types classes inside Zimbra Social.  For that reason it is does require the same version of the .NET framework as your Zimbra Social site.   In addition the SDK handles authentication via Oauth for you meaning with some simple configuration you do not have to implement the Oauth flow yourself.

####Where is the documentation?
Documentation will be in the Developer Documents area for Zimbra Social at community.zimbra.com.   Also there is another repo with several examples you can use as a starting point for your project.

####How do I report a bug?
