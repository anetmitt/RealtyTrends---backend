using Domain.Base;

namespace Domain.App;

public class Website : DomainEntityId
{
    public string Url { get; set; } = default!;
    public string Name { get; set; } = default!;
    
    public ICollection<Property>? Properties { get; set; }
    
    public ICollection<WebCrawler>? WebCrawlers { get; set; }
}