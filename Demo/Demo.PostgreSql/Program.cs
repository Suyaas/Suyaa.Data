// See https://aka.ms/new-console-template for more information
using Demo.PostgreSql.Entities;
using Suyaa.Data.Enums;

Console.WriteLine("Hello, World!");

string _connString = "server=10.10.100.11;port=5432;database=yan_xin;username=dbadmin;password=123456";

using var work = sy.Data.CreateWork(DatabaseType.PostgreSQL, _connString);
var repository = sy.Data.CreateRepository<People, string>(work);
repository.Insert(new People() { Age = 10, Name = "张三" });
work.Commit();