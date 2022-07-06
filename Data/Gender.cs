using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParentCapitalBlazor.Data
{
    public class Gender
    {
        public int Id { get; set; }

        [DisplayName("Пол")]
        public string Name { get; set; }
    }
}
