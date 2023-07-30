using CleanArchitectureSample.ApplicationCore.Domain.Entities;

namespace CleanArchitectureSample.Infrastructure.Mapping
{
    internal static class CodeItemExtentions
    {
        public static CodeItem ToDomainEntity(this Entities.CodeItem dataEntity, CodeItem dto)
        {
            dto.CodeId = dataEntity.CodeId;
            dto.Category = dataEntity.Category;
            dto.CodeName = dataEntity.CodeName;
            dto.CodeValue = dataEntity.CodeValue;
            dto.SeqNo = dataEntity.SeqNo;
            dto.Remarks = dataEntity.Remarks;

            return dto;
        }
        public static Entities.CodeItem ToDataEntity(this CodeItem dto, Entities.CodeItem dataEntity)
        {
            dataEntity.CodeId = dto.CodeId;
            dataEntity.Category = dto.Category;
            dataEntity.CodeName = dto.CodeName;
            dataEntity.CodeValue = dto.CodeValue;
            dataEntity.SeqNo = dto.SeqNo;
            dataEntity.Remarks = dto.Remarks;

            return dataEntity;
        }
    }
}
