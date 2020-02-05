# SelfHost-APITesting
This project is a simple .Net Framework WebApi application with API tests using [Moq](https://github.com/moq/moq) and [OWIN Self Host](https://docs.microsoft.com/en-us/aspnet/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api)

During the test execution, the application is Self Hosted with custom configurations (filters, handlers and even mocked interfaces on the dependency injection container) so we can use the HttpClient library to call our APIs endpoints and test its responses
