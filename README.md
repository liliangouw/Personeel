前言：大学毕业设计，算是人生第一个完整的系统，基本上是从0开始，系统还有不完善的地方，比如有很多地方写的太“死”，以及没有对权限进行很好的控制，预期在学习一段时间后再对项目进行升级完善。
开发工具：vs2022+sqlserver20199

# 技术框架
前端：Bootstrap  
核心框架：ASP.NET  MVC  
ORM框架：Entity Framework  
数据库支持：SqlServer  

# 项目结构
DTO层：业务层DTO对象  
MODEL层：数据库实体层，使用EF的CodeFirst模式创建数据库  
DAL层：数据访问层  
IDAL层：数据访问层接口  
BLL层：业务逻辑层  
IBLL层：业务逻辑层接口  
MVCSite层：前端UI界面
