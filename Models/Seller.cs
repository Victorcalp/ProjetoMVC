using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")] //campo obrigatorio
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do {0} deve ser entre {2} e {1}")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Entre com e-mail valido")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Range(100.00, 50000.0, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        [Required(ErrorMessage = "{0} required")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Base Salary")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; } //criado por causa da View, para puxar o Id do BD
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public Seller() { }

        public void AddSales(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }

        public void RemoveSales(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }

        public double TotalSales(DateTime initial, DateTime finish)
        {
            return Sales.Where(x => x.Date >= initial && x.Date <= finish).Sum(x => x.Amount);
        }
    }
}
