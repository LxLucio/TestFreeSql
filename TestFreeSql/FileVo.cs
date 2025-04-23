using System.ComponentModel;

namespace TestFreeSql;

/// <summary>
/// 文件传输对象
/// </summary>
public class FileVo
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Description("请求路径")]
    public long Id { get; set; }

    /// <summary>
    /// 请求路径
    /// </summary>
    [Description("请求路径")]
    public string RequestPath { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    [Description("文件名")]
    public string FileName { get; set; }
}