using Common;
namespace Domain;

public class Wallet : BaseAuditableEntity
{
    public string UserId { get; set; }
    public decimal Balance { get; set; }
}
