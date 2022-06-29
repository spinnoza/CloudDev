﻿## Create DbContext Migrations

Add-Migration InitialCreate -Context DeviceCenterDbContext -Project ZeroStack.DeviceCenter.Infrastructure -StartupProject ZeroStack.DeviceCenter.Infrastructure

Update-Database -Context DeviceCenterDbContext -Project ZeroStack.DeviceCenter.Infrastructure -StartupProject ZeroStack.DeviceCenter.Infrastructure




