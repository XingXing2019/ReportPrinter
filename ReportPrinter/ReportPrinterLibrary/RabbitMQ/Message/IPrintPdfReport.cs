namespace ReportPrinterLibrary.RabbitMQ.Message
{
    public interface IPrintPdfReport : IPrintReport
    {
        bool HasReprintFlag { get; }
    }
}