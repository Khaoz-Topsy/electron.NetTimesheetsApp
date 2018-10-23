namespace Entelect.TimesheetsToHollardJira.Domain.Result
{
    public class ResultBase
    {
        public bool IsSuccess { get; set; }
        public bool HasFailed => !IsSuccess;
        public string ExceptionMessage { get; set; }

        public ResultBase(bool isSuccess, string exceptionMessage)
        {
            IsSuccess = isSuccess;
            ExceptionMessage = exceptionMessage;
        }

        public override string ToString()
        {
            return $"Success: {IsSuccess}, ExceptionMessage: {ExceptionMessage}";
        }
    }
}
