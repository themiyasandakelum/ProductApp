using ProductAdd.Models;

namespace ProductAdd.Repositary
{
    public interface IData
    {
        Product SaveProductDetails(Product product);
        List<Product> GetAllProductDetails();
        Product UpdateProductDetails (Product product,int productId);
    }
}
