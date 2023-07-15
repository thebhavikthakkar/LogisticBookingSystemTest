using AutoMapper;

public class TimeSpanToStringConverter : ITypeConverter<TimeSpan, string>
{
    public string Convert(TimeSpan source, string destination, ResolutionContext context)
    {
        return source.ToString();
    }
}

public class StringToTimeSpanConverter : ITypeConverter<string, TimeSpan>
{
    public TimeSpan Convert(string source, TimeSpan destination, ResolutionContext context)
    {
        return TimeSpan.Parse(source);
    }
}
