namespace Entelect.TimesheetsToHollardJira.Domain.Result
{
    public class ResultWithValue<T> : ResultBase
    {
        public T Value { get; set; }

        public ResultWithValue(bool isSuccess, T value, string exceptionMessage) : base(isSuccess, exceptionMessage)
        {
            Value = value;
            IsSuccess = isSuccess;
            ExceptionMessage = exceptionMessage;
        }

        public override string ToString()
        {
            return $"Success: {IsSuccess}, ExceptionMessage: {ExceptionMessage}";
        }
    }
}
