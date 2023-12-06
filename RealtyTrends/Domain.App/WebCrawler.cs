using Domain.Base;

namespace Domain.App;

public class WebCrawler : DomainEntityId
{
    public Guid WebsiteId { get; set; }
    public Website? Website { get; set; }
    
    public Guid WebCrawlerParameterId { get; set; }
    public WebCrawlerParameter? WebCrawlerParameter { get; set; }
}