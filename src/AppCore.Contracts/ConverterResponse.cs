namespace AppCore.Contracts;

public class ConverterResponse
{
    public ConverterResponse() { }

    public ConverterResponse(double result) { Result = result; }

    public double Result { get; set; }

    public static Task<ConverterResponse> CreateTaskResoponse(double result)
    {
        return Task.FromResult(new ConverterResponse(result));
    }
}

