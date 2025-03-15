namespace CryptoQuotes.Core;

public class Error
{
    private List<object> _data = new();
    
    public string Code { get; }
    public string Message { get; }
    public IReadOnlyCollection<object> Data => _data;

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public Error AddData(object data)
    {
        _data.Add(data);
        return this;
    }
    
    public static readonly Error None = new Error(string.Empty, string.Empty);
}