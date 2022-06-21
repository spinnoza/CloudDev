using System;
using System.Collections.Generic;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public class Device : BaseEntity<int>
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNo { get; set; } = null!;

        /// <summary>
        /// 安装地址
        /// </summary>
        public DeviceAddress Address { get; set; } = null!;

        /// <summary>
        /// 经纬度
        /// </summary>
        public GeoCoordinate Coordinate { get; set; } = null!;

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool Online { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public Guid ProductId { get; set; } = Guid.Empty;

        /// <summary>
        /// 所属产品
        /// </summary>
        public Product Product { get; set; } = null!;

        /// <summary>
        /// 设备标签
        /// </summary>
        public List<DeviceTag>? Tags { get; set; }
    }
}
