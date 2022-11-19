using AppCore.Contracts;
using System.Diagnostics;

namespace AppCore.Services;

public class ConverterService : IConverterService
{
    private readonly ActivitySource activitySource;

    public ConverterService(ActivitySource activitySource)
    {
        this.activitySource = activitySource;
    }

    public Task<ConverterResponse> KilometersToMiles(double kilometers)
    {
        return this.WithLog(kilometers, nameof(KilometersToMiles), (request) =>
        {
            return Math.Round(kilometers * 0.621371192, 1);
        });
    }

    public Task<ConverterResponse> MilesToKilometers(double miles)
    {
        return this.WithLog(miles, nameof(KilometersToMiles), (request) =>
        {
            return Math.Round(miles * 1.609344, 1);
        });
    }

    private Task<ConverterResponse> WithLog(double number, string operation, Func<double, double> func)
    {
        Activity receiveActivity = this.activitySource.StartActivity($"ConverterService.{operation}", ActivityKind.Internal);

        var returnValue = func(number);
        receiveActivity?.SetEndTime(DateTime.UtcNow);

        receiveActivity?.AddTag("Data.Number", number);
        receiveActivity?.AddTag("Data.Operation", operation);
        receiveActivity?.AddTag("Data.Result", returnValue);

        receiveActivity?.Dispose();
        return ConverterResponse.CreateTaskResoponse(returnValue);
    }
}