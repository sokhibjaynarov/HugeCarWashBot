namespace HugeCarWashBot.Domain.Commons
{
    public class ErrorResponse
    {
        public ErrorResponse(int code, string message = null)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
