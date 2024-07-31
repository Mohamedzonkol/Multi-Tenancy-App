namespace MultiTenancy.API.Data
{
    public class AppDbContext : DbContext
    {
        public string TenantId { get; set; }
        private readonly ITenantServices _tenantServices;

        public AppDbContext(DbContextOptions options, ITenantServices tenantServices) : base(options)
        {
            _tenantServices = tenantServices;
            TenantId = _tenantServices.GetTenant()!.TenantId;
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.TenantId == TenantId);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.TenantId = TenantId;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
