using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParentCapitalBlazor.Data
{
    public class Order
    {
        public int Id { get; set; }

        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayName("Номер")]
        public string Number { get; set; }

        [DisplayName("Дата оплаты")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePay { get; set; }

        [DisplayName("Назначение")]
        public Destination Destination { get; set; }
        public int? DestinationId { get; set; }

        [DisplayName("Сумма")]
        [DataType(DataType.Currency)]
        public decimal Sum { get; set; }

        [DisplayName("Примечание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Сертификат")]
        public Certificate Certificate { get; set; }
        public int CertificateId { get; set; }
    }
}