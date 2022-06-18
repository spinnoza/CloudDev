﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class DeviceCenterEfCoreRepository<TEntity> : EfCoreRepository<DeviceCenterDbContext, TEntity> where TEntity : BaseEntity
    {
        public DeviceCenterEfCoreRepository(DeviceCenterDbContext dbContext) : base(dbContext)
        {

        }
    }

    public class DeviceCenterEfCoreRepository<TEntity, TKey> : EfCoreRepository<DeviceCenterDbContext, TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public DeviceCenterEfCoreRepository(DeviceCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}
