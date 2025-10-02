namespace UPXV.Models;

public class Consumable
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public string? Description { get; set; }
   public ICollection<Tag> Tags { get; set; } = [];
   public double Quantity { get; set; }
   public int UnitNid { get; set; }
   public Unit? Unit { get; set; }
}
