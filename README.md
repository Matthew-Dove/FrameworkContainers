# FrameworkContainers
Useful parts of Frameworks wrapped into single types.  

[PM> Install-Package FrameworkContainers(https://www.nuget.org/packages/FrameworkContainers/)  

### LogContainer

## Error

'Response Error(Action func, string message, params object[] args)'  
Safely run a void function, and log any errors that happen.  

```cs
Response response = FrameworkContainer.Log.Error(() => Save(new Widget()), "Error saving the widget at the time {0}: {1}.", DateTime.Now); // {0} will be the current DateTime, {1} will be the message on the runtime Exception.

void Save(Widget widget)
{
	// Save the widget in a db or something...
}
```

Don't forget to set the stateless error logger at the start of your program.  

```cs
Action<Exception, string, object[]> logger = (ex, message, args) => Console.WriteLine(message, args);
LogContainer.SetErrorLogger(logger);
```