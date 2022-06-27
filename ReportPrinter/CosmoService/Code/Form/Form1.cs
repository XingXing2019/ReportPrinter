using System;
using System.Collections.Generic;
using CosmoService.Code.Producer;
using CosmoService.Code.Producer.PrintReportCommand;
using MassTransit;
using ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.Message;
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
            var producer = PrintReportProducerFactory.CreatePrintReportProducer(QueueName.PDF_QUEUE, new EfPrintReportMessageManager());
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
            var mgr = new EfPrintReportMessageManager();
            //await mgr.Delete(new Guid("66A89C35-5001-4AAD-865F-11F7DD1AAD55"));

            await mgr.PatchStatus(new Guid("B3E374F8-0B4B-4C42-8719-288F827C65BE"), MessageStatus.Receive);
        }
    }
}
