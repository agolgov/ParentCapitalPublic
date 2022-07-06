using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParentCapitalBlazor.Data
{
    public class Destination
    {
        public int Id { get; set; }

        [DisplayName("Направление средств маткапитала")]
        public string Name { get; set; }
    }
}
