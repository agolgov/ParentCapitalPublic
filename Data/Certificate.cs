using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParentCapitalBlazor.Data
{
    public class Certificate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите номер сертификата")]
        [DisplayName("Номер")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Укажите дату включения в регистр")]
        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayName("ФИО")]
        public string Parent { get; set; }

        [DisplayName("Пол")]
        public Gender Gender { get; set; }
        public int? GenderId { get; set; }

        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Адрес")]
        public string Address { get; set; }

        [DisplayName("Документ")]
        public string Document { get; set; }

        [DisplayName("Примечание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Размер капитала")]
        [DataType(DataType.Currency)]
        public decimal Sum { get; set; }

        [DisplayName("Дети")]
        public ICollection<Child> Children { get; set; }
        [DisplayName("Распоряжения")]
        public ICollection<Order> Orders { get; set; }
        [DisplayName("Индексации")]
        public ICollection<Indexation> Indexations { get; set; }

        public Certificate()
        {
            Children = new List<Child>();
            Orders = new List<Order>();
            Indexations = new List<Indexation>();
        }
    }
}