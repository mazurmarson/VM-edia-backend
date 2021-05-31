namespace VM_ediaAPI.Helpers
{
    public class Pagger<T>
    {
         public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PagedList<T> Data { get; set; }
        public Pagger(PagedList<T> _Data)
        {
            Data = _Data;
            CurrentPage = _Data.CurrentPage;
            TotalPages = _Data.TotalPages;
            PageSize = _Data.PageSize;
            TotalCount = Data.TotalCount;
        }
    }
}