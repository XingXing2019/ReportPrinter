using System;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.MessageQueue;
using PrintReportProducerFactory = CosmoService.Code.Producer.PrintReportCommand.PrintReportProducerFactory;

namespace CosmoService.Code.Forms.Message
{
    public partial class frmPublishPrintReport : Form
    {
        private readonly IManager<IPrintReport> _manager;
        public frmPublishPrintReport()
        {
            InitializeComponent();
            _manager = ManagerFactory.CreateManager<IPrintReport>(typeof(IPrintReportMessageManager<IPrintReport>), AppConfig.Instance.DatabaseManagerType);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            var queueName = rdbPDF.Checked ? QueueName.PDF_QUEUE : QueueName.LABEL_QUEUE;
            var producer = PrintReportProducerFactory.CreatePrintReportProducer(queueName, _manager);

            var hasError = false;
            if (rdbPDF.Checked)
            {
                if (!MessageDeserializer<PrintPdfReport>.DeserializeXmlMessage(txtMessage.Text, out var message) || !message.IsValid)
                    hasError = true;
                else
                    await producer.ProduceAsync(message);
            }
            else if (rdbLabel.Checked)
            {
                if (!MessageDeserializer<PrintLabelReport>.DeserializeXmlMessage(txtMessage.Text, out var message) || !message.IsValid)
                    hasError = true;
                else
                    await producer.ProduceAsync(message);
            }

            if (hasError)
            {
                var error = $"There is an error in the input message text, stop sending";
                MessageBox.Show(error);
            }
        }
    }
}
