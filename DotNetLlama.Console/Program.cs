﻿using DotNetLlama.Extensions;
using DotNetLlama.Interfaces;
using DotNetLlama.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Uncomment below to enable trace logging on IRestClient
// Trace.Listeners.Add(new ConsoleTraceListener());
var configuration = new ConfigurationBuilder()
                // This requires creating as it is not saved in GIT
                .AddJsonFile($"appsettings.development.json", true, true)
                .Build();

var builder = Host.CreateApplicationBuilder(args);
builder.Services.RegisterDotNetLlama(configuration);
var hostAppBuilder = builder.Build();

Console.WriteLine("Finished Setup...");
Console.WriteLine("Calling Ollama...");

var ollamaRequest = new OllamaRequest()
{
    Model = "llama3.2",
    Prompt = "Why is the sky blue?"
};

var ollamaRest = hostAppBuilder.Services.GetRequiredService<IOllamaRestClient>();
var ollamaResponse = await ollamaRest.GenerateCompletion(ollamaRequest);

var serializer = hostAppBuilder.Services.GetRequiredService<IJsonSerializer>();
Console.WriteLine("Have response from Ollama...");
Console.WriteLine($"Success: {ollamaResponse.Success}");
Console.WriteLine("Response:");
Console.WriteLine(serializer.Serialize(ollamaResponse.Response));
Console.WriteLine(ollamaResponse.Response?.Response);
