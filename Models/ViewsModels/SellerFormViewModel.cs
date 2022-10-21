namespace ProjetoMVC.Models.ModelViews
{
    public class SellerFormViewModel
    {
        //classe que contem o formulario para cadastro do vendedor
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
