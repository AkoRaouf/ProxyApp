# ProxyApp
### Log Proxy API structure

![proj](./images/proj.PNG)

The solution as you can seen in the above picture, contain 4 projects.

* LogProxy.App: A Web API project contains the hosting process.
* LogProxy.Common : A Class Library project contains common stuff among other projects.
* LogProxy.Core : A Class Library project contains routing to target.
* LogProxy.Test : A test project that test whole main process.

This API has written based on .NET Core 3.1, the main project is `LogProxy.App` that is based in Web API, the whole application process has been implemented using middlewares.
There are two main middlewares, one for handling the Authentication (`BasicAuthMiddleware`) and the other one (`ProxyMiddleware`) is for Proxy logic.
