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
        var entities = new List<DemoEntity>()
        {
            new DemoEntity() { Name = "111" },
            new DemoEntity() { Name = "222" },
        };

        var demoEntities1 = await _freeSql.Insert(entities).ExecuteInsertedAsync();
        Assert.True(demoEntities1.All(a => a.Id > 0));
    }
    
    /// <summary>
    /// 2
    /// </summary>
    [Test]
    public async Task Test2()
    {
        var entities = new List<DemoEntity>()
        {
            new DemoEntity() { Name = "111" },
            new DemoEntity() { Name = "222" },
        };

        var repository = _freeSql.GetRepository<DemoEntity>();
        var demoEntities2 = await repository.InsertAsync(entities);
        Assert.True(demoEntities2.All(a => a.Id > 0));
    }
}