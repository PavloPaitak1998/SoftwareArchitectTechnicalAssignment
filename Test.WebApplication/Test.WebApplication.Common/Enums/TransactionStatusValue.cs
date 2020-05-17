namespace Test.WebApplication.Common.Enums
{
    public enum TransactionStatusValue
    {
        Approved = 1,
        Failed   = 2,
        Finished = 3,
        Rejected = 4,
        Done     = 5
    }

    public static class TransactionStatusValueExtensions
    {
        public static string ToUnifiedFormat(this TransactionStatusValue value) => value switch
        {
            TransactionStatusValue.Finished => "D",
            TransactionStatusValue.Failed => "R",
            _ => value.ToString().Substring(0, 1)
        };
    }
}
