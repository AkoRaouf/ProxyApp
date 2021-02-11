# ProxyApp
### Log Proxy API structure
This API has written based on .NET Core 3.1, the main project is `LogProxy.App` that is based in Web API, the whole application process has been implemented using middlewares.
There are two main middlewares, one for handling the Authentication (`BasicAuthMiddleware`) and the other one (`ProxyMiddleware`) is for Proxy logic.

![Project Structure](https://github.com/AkoRaouf/ProxyApp/blob/master/images/proj.png?raw=true)
 
![proj](./images/proj.png)
