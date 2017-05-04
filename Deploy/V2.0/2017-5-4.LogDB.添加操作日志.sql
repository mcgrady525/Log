USE LogDB;
GO

--添加操作日志
if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_operate_log')
            and   type = 'U')
   drop table t_logs_operate_log
GO

/*==============================================================*/
/* Table: t_logs_operate_log                                    */
/*==============================================================*/
create table t_logs_operate_log (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   machine_name         nvarchar(64)         null,
   ip_address           nvarchar(32)         null,
   process_id           int                  null,
   process_name         nvarchar(128)        null,
   thread_id            int                  null,
   thread_name          nvarchar(128)        null,
   appdomain_name       nvarchar(512)        null,
   operated_time        datetime             null,
   user_id              nvarchar(32)         null,
   user_name            nvarchar(32)         null,
   operate_module       nvarchar(64)         null,
   operate_type         nvarchar(32)         null,
   modify_before        varbinary(max)       null,
   modify_after         varbinary(max)       null,
   created_time         datetime             null,
   constraint PK_T_LOGS_OPERATE_LOG primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_operate_log') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_operate_log' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '操作日志', 
   'user', @CurrentUser, 'table', 't_logs_operate_log'
go