namespace CleanArchitectureSample.Utilities
{
    public class ErrorInfo
    {
        public string Code { set; get; }
        public string Message { set; get; }
        public ErrorInfo() { }
        public ErrorInfo(string errorCode, string errorMsg) { Code = errorCode; Message = errorMsg; }
    }
}
