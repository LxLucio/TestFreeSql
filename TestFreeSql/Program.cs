// See https://aka.ms/new-console-template for more information

using FreeSql;
using TestFreeSql;

// 直接运行没有问题，到单元测试就有异常了。

File.Delete("demo.db");

SQLitePCL.Batteries.Init();


var freeSqlBuilder = new FreeSqlBuilder()
    .UseConnectionString(DataType.Sqlite, "Data Source=demo.db")
    .UseAutoSyncStructure(false)
    .UseLazyLoading(false)
    .UseNoneCommandParameter(true);

var freeSql = freeSqlBuilder.Build();
freeSql.CodeFirst.SyncStructure<DemoEntity>();
var repository = freeSql.GetRepository<DemoEntity>();


var entities = new List<DemoEntity>()
{
    new DemoEntity() { Name = "111" },
    new DemoEntity() { Name = "222" },
};
var demoEntities1 = await freeSql.Insert(entities).ExecuteInsertedAsync();
Console.WriteLine($"检查主键是否正常{demoEntities1.All(a => a.Id > 0)}");

entities =
[
    new DemoEntity() { Name = "111" },
    new DemoEntity() { Name = "222" }
];

var demoEntities2 = await repository.InsertAsync(entities);
Console.WriteLine($"检查主键是否正常{demoEntities2.All(a => a.Id > 0)}");

entities =
[
    new DemoEntity() { Name = "111" },
    new DemoEntity() { Name = "222" }
];
var demoEntities3 = await freeSql.Insert(entities).ExecuteInsertedAsync();
Console.WriteLine($"检查主键是否正常{demoEntities3.All(a => a.Id > 0)}");

entities =
[
    new DemoEntity() { Name = "111" },
    new DemoEntity() { Name = "222" }
];
var demoEntities4 = await freeSql.Insert(entities).ExecuteInsertedAsync();
Console.WriteLine($"检查主键是否正常{demoEntities4.All(a => a.Id > 0)}");