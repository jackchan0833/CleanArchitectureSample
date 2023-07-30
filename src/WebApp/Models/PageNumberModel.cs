namespace CleanArchitectureSample.WebApp.Models
{
    public class PageNumberModel
    {
        public PageNumberModel() { }
        public PageNumberModel(int totalCount, int pageSize, int currentPageNo, string pageIndexUrl)
        {
            this.TotalCount = totalCount;
            this.PageSize = PageSize;
            this.CurrentPageNo = currentPageNo;
            this.PageIndexUrl = pageIndexUrl;
        }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPageNo { get; set; }
        public string PageIndexUrl { get; set; }
        public bool IsAjaxLoad { set; get; }
        /// <summary>
        /// only enabled when IsAjaxLoad is true
        /// </summary>
        public string AjaxCallbackJsFunctionName { set; get; }
    }
}