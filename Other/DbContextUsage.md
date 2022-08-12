
 
# 1. EntityFrameworkCore を使ったDBファースト開発の手始め
## 1.1 Visual studio 2019 必要なもの、以下をNugetで取得
- Microsoft.EntityFrameworkCore

- Microsoft.EntityFrameworkCore.SqlServer

- Microsoft.EntityFrameworkCore.Tools

## 1.2 環境面
- windows 10 home
- Sql Server 2017


# 2. Scaffold-DbContext にてスキーマからモデルとコンテキストの生成
以下、コマンドの全体

Scaffold-DbContext 

"Server=localhost;Database=master;Trusted_Connection=True;Initial Catalog=PlayGround;" 

Microsoft.EntityFrameworkCore.SqlServer 

-OutputDir PlayDbContext\Models 

-Context PlayDbContext 

-Tables [dbo].[Student],[dbo].[Student2] 

-Project Commons 
-Force -UseDatabaseNames


