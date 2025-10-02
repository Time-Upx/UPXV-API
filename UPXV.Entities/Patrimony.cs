namespace UPXV.Models;

public class Patrimony
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public string? Description { get; set; }
   public ICollection<Tag> Tags { get; set; } = [];
   public DateTime RegisteredIn { get; set; } = DateTime.Now;
   public string? RegisteredBy { get; set; }
   public int StatusNid { get; set; }
   public Status? Status { get; set; }
}