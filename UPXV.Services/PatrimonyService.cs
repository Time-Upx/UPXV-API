using FluentValidation;
using FluentValidation.Results;
using UPXV.Common;
using UPXV.Common.Extensions;
using UPXV.Common.Page;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.DTOs.Patrimonies;
using UPXV.Models;
using UPXV.Services.Exceptions;

namespace UPXV.Services;

public sealed class PatrimonyService
{
   private PatrimonyRepository _repository;
   private IValidator<Patrimony> _validator;

   public PatrimonyService (
      PatrimonyRepository repository,
      IValidator<Patrimony> validator
   )
   {
      _repository = repository;
      _validator = validator;
   }

   public Result<PatrimonyDetailDTO, Exception> Create (PatrimonyCreateDTO dto)
   {
      if (_repository.FindBy(c => c.Tid == dto.Tid) is not null)
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }

      List<int> tagNids = dto.TagNids.ToList();
      ICollection<Tag> tags = _repository.ReadQuery(new Query<Tag>()
         .Filter(t => tagNids.Contains(t.Nid)));

      Patrimony patrimony = dto.BuildEntity(tags);

      var validationResult = _validator.Validate(patrimony);
      if (!validationResult.IsValid)
      {
         return new ValidationException(validationResult.Errors);
      }

      _repository.Create(patrimony);
      _repository.Save();

      _repository.Load(patrimony, c => c.Status);
      return PatrimonyDetailDTO.Of(patrimony);
   }

   public Result<PatrimonyDetailDTO, Exception> Update (PatrimonyUpdateDTO dto)
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

      Patrimony? patrimony = _repository.FindByNid(dto.Nid);
      if (patrimony is null) return new EntityNotFoundException<Patrimony>(dto.Nid);

      dto.UpdateEntity(patrimony, tags);

      ValidationResult validationResult = _validator.Validate(patrimony);
      if (!validationResult.IsValid)
         return new ValidationException(validationResult.Errors);

      _repository.Update(patrimony);
      _repository.Save();

      _repository.Load(patrimony, c => c.Status);

      return PatrimonyDetailDTO.Of(patrimony);
   }

   public Result<PatrimonyDetailDTO, Exception> Delete (int nid)
   {
      Patrimony? patrimony = _repository.FindByNid(nid);
      if (patrimony is null) return new EntityNotFoundException<Patrimony>(nid);

      _repository.Load(patrimony, c => c.Status);
      _repository.Delete(patrimony);
      _repository.Save();

      return PatrimonyDetailDTO.Of(patrimony);
   }

   public PageDTO<PatrimonyListDTO> List (int pageIndex, int pageSize)
   {
      PageDTO<Patrimony> pageDto = new PageDTO<Patrimony>(pageIndex, pageSize);

      Query<Patrimony> query = new Query<Patrimony>(pageDto);

      ICollection<Patrimony> entities = _repository.ReadQuery(query);

      IPage<PatrimonyListDTO> page = entities
         .Peek(patrimony => _repository.Load(patrimony, p => p.Status))
         .Select(PatrimonyListDTO.Of)
         .ToPage(pageSize);

      return PageDTO<PatrimonyListDTO>.Of(page);
   }

   public Result<PatrimonyDetailDTO, Exception> Get (int nid)
   {
      Patrimony? patrimony = _repository.FindByNid(nid);
      if (patrimony is null)
      {
         return new EntityNotFoundException<Patrimony>(nid);
      }

      _repository.Load(patrimony, c => c.Status);

      return PatrimonyDetailDTO.Of(patrimony);
   }
}