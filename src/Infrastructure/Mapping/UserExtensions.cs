using CleanArchitectureSample.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureSample.Utilities;
using CleanArchitectureSample.Utilities.Constants;

namespace CleanArchitectureSample.Infrastructure.Mapping
{
    internal static class UserExtensions
    {
        public static User ToDomainEntity(this Entities.User dataEntity, User dto)
        {
            dto.UserId = dataEntity.UserId;
            dto.UserName = dataEntity.UserName;
            dto.NickName = dataEntity.NickName;
            dto.Email = dataEntity.Email;
            dto.Address = dataEntity.Address;
            dto.Status = dataEntity.Status;
            return dto;
        }
        public static Entities.User ToDataEntity(this User dto, Entities.User dataEntity)
        {
            dataEntity.UserName = dto.UserName ?? String.Empty;
            dataEntity.NickName = dto.NickName ?? String.Empty;
            dataEntity.Email = dto.Email ?? String.Empty;
            dataEntity.Address = dto.Address ?? String.Empty;
            dataEntity.Status = dto.Status ?? string.Empty;
            return dataEntity;
        }
    }
}
