namespace CryptoQuotes.Core;

public class Error(string code, string message)
{
    private readonly List<object> _data = [];
    
    public string Code { get; } = code;
    public string Message { get; } = message;
    public IReadOnlyCollection<object> Data => _data;

    public Error AddData(object data)
    {
        _data.Add(data);
        return this;
    }
}