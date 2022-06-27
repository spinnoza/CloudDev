﻿## Create DbContext Migrations

Add-Migration InitialCreate -Context DeviceCenterDbContext -Project ZeroStack.DeviceCenter.Infrastructure -StartupProject ZeroStack.DeviceCenter.Infrastructure

Update-Database -Context DeviceCenterDbContext -Project ZeroStack.DeviceCenter.Infrastructure -StartupProject ZeroStack.DeviceCenter.Infrastructure



授权决定流程

AuthorizationHandler => PermissionChecker => *IPermissionValueProvider =>
IPermissionStore => IPermissionGrantRepository  
