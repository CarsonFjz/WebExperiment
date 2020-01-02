using System;
using System.Collections.Generic;
using System.Text;

namespace Basic.CapWithSugarExtension.Options
{
    public class RabbitMqOptions
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string HostName { get; set; } = "localhost";
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 5670;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "guest";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "guest";
    }
}
