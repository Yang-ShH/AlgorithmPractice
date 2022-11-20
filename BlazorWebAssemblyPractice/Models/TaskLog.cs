namespace BlazorWebAssemblyPractice.Models
{
    public class TaskLog
    {
        public int Id { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public short Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
