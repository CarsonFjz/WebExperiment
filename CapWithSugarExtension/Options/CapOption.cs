using System;

namespace Basic.CapWithSugarExtension.Options
{
    public class CapOption
    {
        /// <summary>
        /// 失败重试次数
        /// </summary>
        public int FailedRetryCount { get; set; } = 5;
        /// <summary>
        /// 消费线程数
        /// </summary>
        public int ConsumerThreadCount { get; set; } = 1;
        /// <summary>
        /// 失败重试时间间隔
        /// </summary>
        public int FailedRetryInterval { get; set; } = 60;
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = "v1";
    }
}
