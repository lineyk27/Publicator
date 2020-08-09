namespace Publicator.Core
{
    interface IPageRequest
    {
        int PageSize { get; set; }
        int Page { get; set; }
    }
}
