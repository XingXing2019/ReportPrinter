﻿using System;
using CosmoService.Code.Producer;
using MassTransit;
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
            var producer = CommandProducerFactory.CreateCommandProducer(QueueName.PDF_QUEUE);
            object message = new
            {
                CorrelationId = InVar.Id,
                MessageId = Guid.NewGuid(),
                TemplateId = "template",
                PrinterId = "printer",
                SqlVariables = new[]
                {
                    new { Name = "a", Value = "1" }, 
                    new { Name = "b", Value = "2" },
                },
                NumberOfCopy = 1,
                HasReprintFlag = false
            };

            await producer.Produce(message);
        }
    }
}
