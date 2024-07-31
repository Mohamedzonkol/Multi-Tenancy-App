namespace MultiTenancy.API.Contracts
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
