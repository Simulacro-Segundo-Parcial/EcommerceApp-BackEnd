namespace EcommerceApi.Models
{
    public enum UserRole
    {
        Admin = 0,
        CompanyAdmin = 1,
        Customer = 2
    }

    public enum CompanyStatus
    {
        PendingVerification = 0,
        Verified = 1,
        Rejected = 2
    }

    public enum ProductStatus
    {
        Draft = 0,
        Published = 1
    }
}