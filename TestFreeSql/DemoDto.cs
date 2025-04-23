using FreeSql.DataAnnotations;

namespace TestFreeSql;

public class DemoDto
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 项目文件列表
    /// </summary>
    public List<FileVo>? Files { get; set; }
}