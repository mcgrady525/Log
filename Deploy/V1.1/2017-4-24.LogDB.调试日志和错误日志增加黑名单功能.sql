USE LogDB;
GO

--调试日志和错误日志增加黑名单功能
--t_logs_debug_log_black_list
IF OBJECT_ID('dbo.t_logs_debug_log_black_list') IS NOT NULL
	DROP TABLE dbo.t_logs_debug_log_black_list;
go

/*==============================================================*/
/* Table: t_logs_debug_log_black_list                           */
/*==============================================================*/
create table t_logs_debug_log_black_list (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   machine_name         nvarchar(64)         null,
   ip_address           nvarchar(512)        null,
   client_ip            nvarchar(512)        null,
   appdomain_name       nvarchar(512)        null,
   message              nvarchar(1024)       null,
   is_regex             bit                  null,
   created_by           int                  not null,
   created_time         datetime             not null,
   last_updated_by      int                  null,
   last_updated_time    datetime             null,
   constraint PK_T_LOGS_DEBUG_LOG_BLACK_LIST primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_debug_log_black_list') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '调试日志黑名单。', 
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_logs_debug_log_black_list')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'created_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人id',
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'created_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_logs_debug_log_black_list')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'created_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'created_time'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_logs_debug_log_black_list')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'last_updated_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人id',
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'last_updated_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_logs_debug_log_black_list')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'last_updated_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'last_updated_time'
go


--t_logs_error_log_black_list
IF OBJECT_ID('dbo.t_logs_error_log_black_list') IS NOT NULL
	DROP TABLE dbo.t_logs_error_log_black_list;
GO

/*==============================================================*/
/* Table: t_logs_error_log_black_list                           */
/*==============================================================*/
create table t_logs_error_log_black_list (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   machine_name         nvarchar(64)         null,
   ip_address           nvarchar(512)        null,
   client_ip            nvarchar(512)        null,
   appdomain_name       nvarchar(512)        null,
   message              nvarchar(1024)       null,
   is_regex             bit                  null,
   created_by           int                  not null,
   created_time         datetime             not null,
   last_updated_by      int                  null,
   last_updated_time    datetime             null,
   constraint PK_T_LOGS_ERROR_LOG_BLACK_LIST primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_error_log_black_list') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '错误日志黑名单。', 
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_logs_error_log_black_list')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'created_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人id',
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'created_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_logs_error_log_black_list')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'created_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'created_time'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_logs_error_log_black_list')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'last_updated_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人id',
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'last_updated_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_logs_error_log_black_list')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'last_updated_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'last_updated_time'
go