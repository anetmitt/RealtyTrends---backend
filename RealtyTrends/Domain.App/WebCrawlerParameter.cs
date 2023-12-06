using Domain.Base;

namespace Domain.App;

public class WebCrawlerParameter : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string Value { get; set; } = default!;
    
    public ICollection<WebCrawler>? WebCrawlers { get; set; }
}