using FreeSql;
using Newtonsoft.Json;
using TestFreeSql;

namespace TestProject1;

public class Tests
{
    private IFreeSql _freeSql;

    [OneTimeSetUp]
    public void Setup()
    {
        File.Delete("demo.db");
        SQLitePCL.Batteries.Init();
        _freeSql = new FreeSqlBuilder()
            .UseConnectionString(DataType.Sqlite, "Data Source=demo.db")
            .UseAutoSyncStructure(false)
            .UseLazyLoading(false)
            .UseNoneCommandParameter(true)
            .Build();
        _freeSql.CodeFirst.SyncStructure<DemoEntity>();
    }

    [OneTimeTearDown]
    public void Down()
    {
        _freeSql.Dispose();
    }

    /// <summary>
    /// 1
    /// </summary>
    [Test]
    public async Task Test1()
    {
        var entities1 = new List<DemoEntity>()
        {
            new DemoEntity() { Name = "1111" },
            new DemoEntity() { Name = "11111" },
        };
        var repository = _freeSql.GetRepository<DemoEntity>();

        // 正常使用仓储没问题
        var insertResEntities1 = await repository.InsertAsync(entities1);
        Console.WriteLine($"检查{nameof(entities1)}主键是否正常{insertResEntities1.All(a => a.Id > 0)}");
        Assert.True(insertResEntities1.All(a => a.Id > 0));

        // 使用仓储时如果是惰性集合IEnumerable会出问题，内部应该是直接操作的外部对象，但是Select返回的集合是惰性的此处的DemoEntity对象并没有实例化，应该是在内部时遍历时才创建新对象。
        var entities2 = entities1.Select(a => new DemoEntity() { Name = "222" });
        entities2 = await repository.InsertAsync(entities2);
        Assert.True(entities2.All(a => a.Id > 0));

        // 不使用仓储，如果是自增主键无论怎样都无法返回Id，尝试过使用审计方法赋值可以正常返回主键
        var entities3 = entities1.Select(a => new DemoEntity() { Name = "333" });
        entities3 = await _freeSql.Insert(entities3).ExecuteInsertedAsync();
        Assert.True(entities3.All(a => a.Id > 0));
        var entities4 = entities1.Select(a => new DemoEntity() { Name = "444" }).ToList();
        entities4 = await _freeSql.Insert(entities4).ExecuteInsertedAsync();
        Assert.True(entities4.All(a => a.Id > 0));

        // 使用仓储，把惰性的IEnumerable ToList就可以了，这样DemoEntity才会在此处实例化。
        var entities5 = entities1.Select(a => new DemoEntity() { Name = "555" }).ToList();
        entities5 = await repository.InsertAsync(entities5);
        Assert.True(entities5.All(a => a.Id > 0));
    }
}