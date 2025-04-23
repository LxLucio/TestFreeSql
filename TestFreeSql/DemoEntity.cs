using FreeSql.DataAnnotations;

namespace TestFreeSql;

[Table(Name = "demo")]
public class DemoEntity
{
    /// <summary>
    /// ID
    /// </summary>
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// 项目文件列表
    /// </summary>
    public string Name { get; set; }
}