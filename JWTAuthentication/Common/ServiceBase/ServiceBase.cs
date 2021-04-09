using AutoMapper;
using JWTAuthentication.Entities;
using JWTAuthentication.Repository;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Common.ServiceBase
{
    public class ServiceBase<T> : IServiceBase<T>
    where T : BaseEntity
    {
        public ServiceBase(IAsyncRepository<T> entityRepository,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            EntityRepository = entityRepository;
            HttpContextAccessor = httpContextAccessor;
            Mapper = mapper;
        }
        public IMapper Mapper { get; }
        public IAsyncRepository<T> EntityRepository { get; }

        public IHttpContextAccessor HttpContextAccessor { get; }


        public virtual ValidationResult DoSubmit(T entity)
        {
            var tran = EntityRepository.BeginTransaction();
            var result = new ValidationResult();

            object entityId = 0;
            try
            {
                result = SubmitValidation(entity);
                entityId = entity.Id;
                if (result.State == ValidationResultState.IsValid)
                {

                    Submit(entity, entityId);
                    if (tran != null)
                    {
                        result.Entity = entity;
                        tran.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                result = new ValidationResult(e.Message);
            }
            finally
            {

            }
            return result;
        }

        private void Submit(T entity, object entityId)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            if (entityId.ToString() != "0")
            {
                EntityRepository.UpdateAsync(entity);
                var json = JsonConvert.SerializeObject(entity, jsonSetting);
            }
            else
            {
                var createDateProp = typeof(T).GetProperty("CreateDate");
                if (createDateProp != null)
                {
                    createDateProp.SetValue(entity, DateTime.Now);
                }
                var userIdProp = typeof(T).GetProperty("UserId");
                if (userIdProp != null && userIdProp.PropertyType == typeof(int) && HttpContextAccessor.HttpContext.User.Identity?.Name != null)
                {
                    var userId = int.Parse(HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(n => n.Type == "UserId").Value);
                    userIdProp.SetValue(entity, userId);
                }
                EntityRepository.Add(entity);
                var json = JsonConvert.SerializeObject(entity, jsonSetting);
            }
        }
        public virtual ValidationResult SubmitValidation(T entity)
        {
            return new ValidationResult();
        }

        public async Task<T> LoadEntity(int entityId)
        {
            return await EntityRepository.GetByIdAsync(entityId);
        }

        public async Task DeleteEntity(int entityId)
        {
            var entity = await LoadEntity(entityId);
            EntityRepository.Delete(entity);
        }

    }
}
