using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParentCapitalBlazor.Data
{
    public class Child
    {
        public int Id { get; set; }

        [DisplayName("ФИО")]
        public string Name { get; set; }

        [DisplayName("Пол ребенка")]
        public Gender Gender { get; set; }
        public int? GenderId { get; set; }

        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Место рождения")]
        public string PlaceOfBirth { get; set; }

        [DisplayName("Документ")]
        public string Document { get; set; }

        [DisplayName("Гражданство")]
        [DefaultValue("РФ")]
        public string Citizenship { get; set; }

        [DisplayName("Адрес")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [DisplayName("Сертификат")]
        public Certificate Certificate { get; set; }
        public int CertificateId { get; set; }
    }
}