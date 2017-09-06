USE LogDB;
GO

--添加操作日志智能提示表
if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_operate_log_tip')
            and   type = 'U')
   drop table t_logs_operate_log_tip
GO

/*==============================================================*/
/* Table: t_logs_operate_log_tip                                */
/*==============================================================*/
create table t_logs_operate_log_tip (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   operate_module       nvarchar(64)         null,
   operate_type         nvarchar(32)         null,
   created_time         datetime             null,
   modified_time        datetime             null,
   constraint PK_T_LOGS_OPERATE_LOG_TIP primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_operate_log_tip') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_operate_log_tip' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '操作日志智能提示', 
   'user', @CurrentUser, 'table', 't_logs_operate_log_tip'
go