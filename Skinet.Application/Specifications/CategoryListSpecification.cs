using Skinet.Entities.Product;

namespace Skinet.Application.Specifications;

public class CategoryListSpecification : BaseSpecification<Product, string>
{
    public CategoryListSpecification()
    {
        AddSelect(x => x.Category);
        ApplyDistinct();
    }
}
