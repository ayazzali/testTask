namespace TestTask;

public class SearchService : ISearchService
{
    public Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}