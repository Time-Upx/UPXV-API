using FluentValidation;
using FluentValidation.Results;
using UPXV.Common;
using UPXV.Common.Extensions;
using UPXV.Common.Page;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.DTOs.Consumables;
using UPXV.Models;
using UPXV.Services.Exceptions;

namespace UPXV.Services;

public sealed class ConsumableService
{
   private ConsumableRepository _repository;
   private IValidator<Consumable> _validator;

   public ConsumableService (
      ConsumableRepository repository,
      IValidator<Consumable> validator
   ) {
      _repository = repository;
      _validator = validator;
   }

   public Result<ConsumableDetailDTO, Exception> Create (ConsumableCreateDTO dto)
   {
      if (_repository.FindBy(c => c.Tid == dto.Tid) is not null)
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }

      List<int> tagNids = dto.TagNids.ToList();
      ICollection<Tag> tags = _repository.ReadQuery(new Query<Tag>()
         .Filter(t => tagNids.Contains(t.Nid)));

      Consumable consumable = dto.BuildEntity(tags);

      var validationResult = _validator.Validate(consumable);
      if (!validationResult.IsValid)
      {
         return new ValidationException(validationResult.Errors);
      }

      _repository.Create(consumable);
      _repository.Save();

      _repository.Load(consumable, c => c.Unit);

      return ConsumableDetailDTO.Of(consumable);
   }

   public Result<ConsumableDetailDTO, Exception> Update (ConsumableUpdateDTO dto)
   {
      if (_repository.FindBy(c => c.Nid != dto.Nid && c.Tid == dto.Tid) is not null)
      {
         ValidationFailure failure = new ValidationFailure(
            nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }

      ICollection<Tag>? tags = null;
      if (dto.TagNids != null)
      {
         var tagNids = dto.TagNids.ToList();
         tags = _repository.ReadQuery(new Query<Tag>().Filter(t => tagNids.Contains(t.Nid)));
      }

      Consumable? consumable = _repository.FindByNid(dto.Nid);
      if (consumable is null) return new EntityNotFoundException<Consumable>(dto.Nid);

      dto.UpdateEntity(consumable, tags);

      ValidationResult validationResult = _validator.Validate(consumable);
      if (!validationResult.IsValid)
         return new ValidationException(validationResult.Errors);

      _repository.Update(consumable);
      _repository.Save();

      _repository.Load(consumable, c => c.Unit);

      return ConsumableDetailDTO.Of(consumable);
   }

   public Result<ConsumableDetailDTO, Exception> Delete (int nid)
   {
      Consumable? consumable = _repository.FindByNid(nid);
      if (consumable is null) return new EntityNotFoundException<Consumable>(nid);

      _repository.Load(consumable, c => c.Unit);
      _repository.Delete(consumable);
      _repository.Save();

      return ConsumableDetailDTO.Of(consumable);
   }

   public PageDTO<ConsumableListDTO> List (int pageIndex, int pageSize)
   {
      PageDTO<Consumable> pageDto = new PageDTO<Consumable>(pageIndex, pageSize);

      Query<Consumable> query = new Query<Consumable>(pageDto);

      ICollection<Consumable> entities = _repository.ReadQuery(query);

      IPage<ConsumableListDTO> page = entities
         .Peek(consumable => _repository.Load(consumable, c => c.Unit))
         .Select(ConsumableListDTO.Of)
         .ToPage(pageSize);

      return PageDTO<ConsumableListDTO>.Of(page);
   }

   public Result<ConsumableDetailDTO, Exception> Get (int nid)
   {
      Consumable? consumable = _repository.FindByNid(nid);
      if (consumable is null)
      {
         return new EntityNotFoundException<Consumable>(nid);
      }

      _repository.Load(consumable, c => c.Unit);

      return ConsumableDetailDTO.Of(consumable);
   }
}