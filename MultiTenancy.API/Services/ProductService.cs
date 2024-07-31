namespace MultiTenancy.API.Services
{
    public class ProductService(AppDbContext context) : IProductService
    {
        public async Task<Product> CreatedAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }
    }
}
