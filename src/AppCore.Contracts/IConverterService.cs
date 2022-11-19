namespace AppCore.Contracts;

public interface IConverterService
{
    Task<ConverterResponse> KilometersToMiles(double kilomenters);
    Task<ConverterResponse> MilesToKilometers(double miles);
}