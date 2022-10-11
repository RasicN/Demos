namespace WebHost.Models;

public class ProductReview
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public double Rating { get; set; }
    public string? Comments { get; set; }
    public Guid UserId { get; set; }
    public bool VerifiedPurchase { get; set; }

}