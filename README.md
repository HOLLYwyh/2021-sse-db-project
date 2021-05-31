# 2021-sse-db-project
2021-sse-db-project

##  数据库课程项目
**特别注意！！！（尤其是后端的同学！！）**

考虑到代码安全性问题，最新版的代码隐藏了包配置文件packages.config,Web配置文件Web.config、  Web.Debug.config、Web.Release.config

所以在本地实际运行时候你需要

1. 工具->NuGet包管理->管理解决方案的NuGet程序包  

   搜索并安装Oracle.ManagedDataAccess.EntityFramework(注意没有core)  

2. 在Web.config文件中<system.web></system.web>中添加如下代码：  

   ```html
   <globalization fileEncoding="utf-8" requestEncoding="utf-8" responseEncoding="utf-8" />
       <!--设置页面整体编码格式-->
       <customErrors mode="On">
         <!--添加自定义错误页面-->
         <error statusCode="404" redirect="/Error/Error404" />
         <!--404-->
       </customErrors>
   ```

3. 找到Web.config文件，修改信息以配置数据库  

   ```html
     <connectionStrings>
       <add name="OracleDbContext" providerName="Oracle.ManagedDataAccess.Client" connectionString="User Id=****;Password=*****;Data Source=oracle" />
     </connectionStrings>
   <!--以上的User Id写Oracle的用户名，Password写用户密码 Source后的字符串换成(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=数据库IP地址)(PORT=端口号))(CONNECT_DATA=(SERVICE_NAME=服务名))) -->
   
   <oracle.manageddataaccess.client>
       <version number="*">
         <dataSources>
           <dataSource alias="*****" descriptor="(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=*****)(PORT=***))(CONNECT_DATA=(SERVICE_NAME=**))) " />
         </dataSources>
       </version>
     </oracle.manageddataaccess.client>
   <!--以上的alias写Oracle用户名，HOST写IP地址，PORT写对应端口，SERVICE_NAME写服务名-->
   ```

   
