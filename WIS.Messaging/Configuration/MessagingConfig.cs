namespace WIS.Messaging.Configuration;

public class MessagingConfig
{
    public string ServiceUrl { get; set; }
    public  Settings Producer { get; init; }
    public  Settings Consumer { get; init; }
}