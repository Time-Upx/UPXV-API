using UPXV.Common.Extensions;
using UPXV.Common.Page;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.DTOs.Items;
using UPXV.Models;

namespace UPXV.Services;

public sealed class ItemService
{
   private ConsumableRepository _consumableRepository;
   private PatrimonyRepository _patrimonyRepository;

   public ItemService (ConsumableRepository consumableRepository, PatrimonyRepository patrimonyRepository)
   {
      _consumableRepository = consumableRepository;
      _patrimonyRepository = patrimonyRepository;
   }

   public PageDTO<ItemListDTO> List (int pageIndex, int pageSize)
   {
      ICollection<Consumable> consumables = _consumableRepository.ReadQuery(
         new Query<Consumable>().Include(c => c.Unit));

      ICollection<Patrimony> patrimonies = _consumableRepository.ReadQuery(
         new Query<Patrimony>().Include(p => p.Status));

      IEnumerable<ItemListDTO> items = consumables
         .Select(ItemListDTO.Of)
         .Concat(patrimonies.Select(ItemListDTO.Of))
         .OrderBy(i =>
         {
            if (i.Consumable is not null) return i.Consumable.Tid;
            if (i.Patrimony is not null) return i.Patrimony.Tid;
            return "";
         });

      return new PageDTO<ItemListDTO>(items, pageIndex, pageSize);
   }
}
