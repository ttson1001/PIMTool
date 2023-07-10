namespace PIMTool.Dtos
{
    public class SendResponseDto
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public SendResponseDto()
        {
        }

        public SendResponseDto(int? statusCode, string? message, object? data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }


    }
}
