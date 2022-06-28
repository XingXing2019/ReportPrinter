using System;
using System.Collections.Generic;
using CosmoService.Code.Producer.PrintReportCommand;
using MassTransit;
using ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.MessageQueue;

namespace CosmoService.Code.Form
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var producer = PrintReportProducerFactory.CreatePrintReportProducer(QueueName.PDF_QUEUE, new PrintReportMessageManager());
            var message = new PrintPdfReport
            {
                CorrelationId = InVar.Id,
                MessageId = Guid.NewGuid(),
                TemplateId = "template",
                PrinterId = "printer",
                SqlVariables = new List<SqlVariable>
                {
                    new SqlVariable { Name = "a", Value = "1" },
                    new SqlVariable { Name = "b", Value = "2" },
                },
                NumberOfCopy = 2,
                HasReprintFlag = false
            };

            await producer.ProduceAsync(message);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var mgr = new PrintReportMessageManager();
            await mgr.DeleteAll();
        }
    }
}
