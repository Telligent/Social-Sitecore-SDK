# Sitecore SDK for Zimbra Social
##Sitecore Social SDK
>###***BETA Disclaimer
This Sitecore SDK for Zimbra Social is in beta and is subject to change at any time. It is not currently appropriate for production use. The beta version is appropriate for testing and initial development. We expect it will be production ready late March or early April 2015.

####Dependencies
The Sitecore SDK requires that you have installed and are running Zimbra Social (minimum version 8.5) and Sitecore (minimum version 7.5) or have access to cloud/SaaS versions of these products.  The Sitecore SDK for Zimbra Social has dependencies on libraries that, for licensing reasons, are included.  You will need to obtain the libraries listed below and copy them to the */lib* folder before compiling:

**Sitecore**
- Sitecore 7.5 or higher
- Sitecore.Client.dll
- Sitecore.Kernel.dll

**Zimbra Social**
- Zimbra Social (free or commercial) 8.0 or higher
- [Zimbra Social REST SDK](https://github.com/Zimbra/Social-Rest-SDK/)

**.NET Framework**
- version 4.5 or higher

###What is the Sitecore SDK for Zimbra Social?
The Sitecore SDK for Zimbra Social is a framework to allow you to easily interact with Zimbra Social content from inside Sitecore. The entire Zimbra Social platform is available through these APIs and allows you to natively add blogs, forums, wikis, comments, ratings and more natively running within Sitecore. Samples are available that demonstrate how this can be done [Sitecore SDK for Zimbra Social samples](https://github.com/Zimbra/Social-SitecoreSDK-Samples).

The Sitecore SDK for Zimbra Social adds Sitecore specific behaviors and functionality around the [Zimbra Social REST SDK](https://github.com/Zimbra/Social-Rest-SDK/).

####Is this the same as Zimbra Connector for Sitecore?
No, Zimbra Connector for Sitecore (last release is version 3) is the predecessor to the Sitecore SDK for Zimbra Social. While the technology behind the Zimbra Connector for Sitecore and the Sitecore SDK for Zimbra Social are the same, the Sitecore SDK for Zimbra Social was built specifically from the feedback of partners and customers that had previously used the Zimbra Connector for Sitecore integration. 

The Zimbra Connector for Sitecore utilized remote widget rendering to allow any Zimbra Social widget to render within Sitecore. While this enabled any Zimbra Social widget to easily run within Sitecore, it limited the control that developers had over the rendering.

The Sitecore SDK for Zimbra Social does not use the remote widget rendering, but instead is designed to give developers more direct access to the RESTful Platform APIs offered by Zimbra Social. 

####Can I still use the Zimbra Social RESTful / Platform APIs directly?
Yes, we believe RESTful APIs are a great way to integrate with any third party site because it is technology independent and because of its use of standard XML and JSON for data transfer. Developers working with Sitecore and Zimbra Social can absolutely still write REST API calls themselves.

However, the Sitecore SDK for Zimbra Social is designed to make this much easier as it abstracts the remoting infrastructure. The Sitecore SDK for Zimbra Social still relies on HTTPS to interact with Zimbra Social, however it is remotely invoking API calls and returning .NET dynamic objects that allow you to interact with the data much like you would the strongly types classes inside Zimbra Social. For that reason it is does require the same version of the .NET framework as your Zimbra Social site. In addition the Sitecore SDK for Zimbra Social handles authentication via OAuth natively and you do not have to implement the OAuth flow yourself.

Additionally, unique Sitecore specific integration will be added to the Sitecore SDK for Zimbra Social over time, such as integration with the Sitecore Experience User Database.

####What about performance?
You still have to consider that the Sitecore SDK for Zimbra Social is dependent on making HTTPS requests to Zimbra Social. Any hinderance in that communication can cause performance issues similar to any out of process call (such as direct REST API calls or interactions with a database).

Underlying the Sitecore SDK for Zimbra Social, the Zimbra Social REST SDK provides features to aid developers in getting optimal performance: API data trimming, async calls and API batching (API batching allows multiple API calls to be made over a single REST request).

####Where is the documentation?
Please refer to the [wiki section](https://github.com/Zimbra/Social-Sitecore-SDK/wiki) of this repository.

####How do I report a bug?
You can use the [issues section](https://github.com/Zimbra/Social-Sitecore-SDK/issues) of this repository to report any issues with the Sitecore SDK for Zimbra Social.

####Can I contribute?
Yes, we will have more details soon on how you can contribute additions to the Sitecore SDK for Zimbra Social.