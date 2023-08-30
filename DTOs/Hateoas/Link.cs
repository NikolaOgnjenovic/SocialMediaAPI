namespace SocialConnectAPI.DTOs.Hateoas;

public class Link
{
    public string Href { get; }
    public string Rel { get; set; }
    public string Method { get; set; }

    public Link(string href, string rel, string method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}