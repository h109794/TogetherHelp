# 项目介绍
基于**ASP.NET MVC**的文章分享平台。
# 示例网站
[https://zhibinye.vip](https://zhibinye.vip)
# 功能简介
- 登录/注册（完成后返回原页面）
- 密码MD5加密
- 密码找回
- 邮箱绑定
- 文章发布/编辑/删除
- 支持自定义文章样式
- 根据内容自动生成摘要
- 关键字检索
- 评论发布/回复
- 内容评价（赞/踩）
# 快速开始
1. 此项目数据库使用SQL Server。
2. 在控制台项目**QuickDb**内找到**SqlDbContext.cs**文件。
3. 在**构造函数**参数内填入空数据库连接字符串或希望建立的数据库名称。
4. 在**BLL**文件夹**Repository**项目**SqlDbContext.cs**内执行上述操作。  
`private SqlDbContext() : base("连接字符串或数据库名称") { }`
5. 运行**QuickDb**，会自动构建数据库并生成简单数据，以供演示使用。
