/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2017/5/5 22:20:34                            */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_debug_log')
            and   type = 'U')
   drop table t_logs_debug_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_debug_log_black_list')
            and   type = 'U')
   drop table t_logs_debug_log_black_list
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_debug_log_tip')
            and   type = 'U')
   drop table t_logs_debug_log_tip
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_error_log')
            and   type = 'U')
   drop table t_logs_error_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_error_log_black_list')
            and   type = 'U')
   drop table t_logs_error_log_black_list
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_error_log_tip')
            and   type = 'U')
   drop table t_logs_error_log_tip
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_operate_log')
            and   type = 'U')
   drop table t_logs_operate_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_operate_log_tip')
            and   type = 'U')
   drop table t_logs_operate_log_tip
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_performance_log')
            and   type = 'U')
   drop table t_logs_performance_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_performance_log_tip')
            and   type = 'U')
   drop table t_logs_performance_log_tip
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_xml_log')
            and   type = 'U')
   drop table t_logs_xml_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_logs_xml_log_tip')
            and   type = 'U')
   drop table t_logs_xml_log_tip
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_button')
            and   type = 'U')
   drop table t_rights_button
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_menu')
            and   type = 'U')
   drop table t_rights_menu
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_menu_button')
            and   type = 'U')
   drop table t_rights_menu_button
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_organization')
            and   type = 'U')
   drop table t_rights_organization
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_role')
            and   type = 'U')
   drop table t_rights_role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_role_menu_button')
            and   type = 'U')
   drop table t_rights_role_menu_button
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_user')
            and   type = 'U')
   drop table t_rights_user
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_user_organization')
            and   type = 'U')
   drop table t_rights_user_organization
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_rights_user_role')
            and   type = 'U')
   drop table t_rights_user_role
go

/*==============================================================*/
/* Table: t_logs_debug_log                                      */
/*==============================================================*/
create table t_logs_debug_log (
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
   message              nvarchar(1024)       null,
   detail               varbinary(max)       null,
   created_time         datetime             null,
   constraint PK_T_LOGS_DEBUG_LOG primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_debug_log') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_debug_log' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '������־', 
   'user', @CurrentUser, 'table', 't_logs_debug_log'
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
   '������־��������', 
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
   '������id',
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
   '����ʱ��',
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
   '����޸���id',
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
   '����޸�ʱ��',
   'user', @CurrentUser, 'table', 't_logs_debug_log_black_list', 'column', 'last_updated_time'
go

/*==============================================================*/
/* Table: t_logs_debug_log_tip                                  */
/*==============================================================*/
create table t_logs_debug_log_tip (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   created_time         datetime             null,
   modified_time        datetime             null,
   constraint PK_T_LOGS_DEBUG_LOG_TIP primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_debug_log_tip') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_debug_log_tip' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '������־������ʾ', 
   'user', @CurrentUser, 'table', 't_logs_debug_log_tip'
go

/*==============================================================*/
/* Table: t_logs_error_log                                      */
/*==============================================================*/
create table t_logs_error_log (
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
   message              nvarchar(1024)       null,
   detail               varbinary(max)       null,
   created_time         datetime             null,
   constraint PK_T_LOGS_ERROR_LOG primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_error_log') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_error_log' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '������־', 
   'user', @CurrentUser, 'table', 't_logs_error_log'
go

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
   '������־��������', 
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
   '������id',
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
   '����ʱ��',
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
   '����޸���id',
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
   '����޸�ʱ��',
   'user', @CurrentUser, 'table', 't_logs_error_log_black_list', 'column', 'last_updated_time'
go

/*==============================================================*/
/* Table: t_logs_error_log_tip                                  */
/*==============================================================*/
create table t_logs_error_log_tip (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   created_time         datetime             null,
   modified_time        datetime             null,
   constraint PK_T_LOGS_ERROR_LOG_TIP primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_error_log_tip') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_error_log_tip' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '������־������ʾ', 
   'user', @CurrentUser, 'table', 't_logs_error_log_tip'
go

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
   '������־', 
   'user', @CurrentUser, 'table', 't_logs_operate_log'
go

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
   '������־������ʾ', 
   'user', @CurrentUser, 'table', 't_logs_operate_log_tip'
go

/*==============================================================*/
/* Table: t_logs_performance_log                                */
/*==============================================================*/
create table t_logs_performance_log (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   machine_name         nvarchar(64)         null,
   ip_address           nvarchar(32)         null,
   process_id           int                  null,
   process_name         nvarchar(128)        null,
   thread_id            int                  null,
   thread_name          nvarchar(128)        null,
   class_name           nvarchar(64)         null,
   method_name          nvarchar(64)         null,
   duration             bigint               null,
   remark               nvarchar(max)        null,
   created_time         datetime             null,
   constraint PK_T_LOGS_PERFORMANCE_LOG primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_performance_log') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_performance_log' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '������־', 
   'user', @CurrentUser, 'table', 't_logs_performance_log'
go

/*==============================================================*/
/* Table: t_logs_performance_log_tip                            */
/*==============================================================*/
create table t_logs_performance_log_tip (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   class_name           nvarchar(64)         null,
   method_name          nvarchar(64)         null,
   created_time         datetime             null,
   modified_time        datetime             null,
   constraint PK_T_LOGS_PERFORMANCE_LOG_TIP primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_performance_log_tip') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_performance_log_tip' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '������־������ʾ', 
   'user', @CurrentUser, 'table', 't_logs_performance_log_tip'
go

/*==============================================================*/
/* Table: t_logs_xml_log                                        */
/*==============================================================*/
create table t_logs_xml_log (
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
   class_name           nvarchar(64)         null,
   method_name          nvarchar(64)         null,
   rq                   varbinary(max)       null,
   rs                   varbinary(max)       null,
   remark               nvarchar(max)        null,
   created_time         datetime             null,
   constraint PK_T_LOGS_XML_LOG primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_xml_log') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_xml_log' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'xml��־(��¼�ӿ��������Ӧ���)', 
   'user', @CurrentUser, 'table', 't_logs_xml_log'
go

/*==============================================================*/
/* Table: t_logs_xml_log_tip                                    */
/*==============================================================*/
create table t_logs_xml_log_tip (
   id                   bigint               identity,
   system_code          nvarchar(32)         null,
   source               nvarchar(64)         null,
   class_name           nvarchar(64)         null,
   method_name          nvarchar(64)         null,
   created_time         datetime             null,
   modified_time        datetime             null,
   constraint PK_T_LOGS_XML_LOG_TIP primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_logs_xml_log_tip') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_logs_xml_log_tip' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'xml��־������ʾ', 
   'user', @CurrentUser, 'table', 't_logs_xml_log_tip'
go

/*==============================================================*/
/* Table: t_rights_button                                       */
/*==============================================================*/
create table t_rights_button (
   id                   int                  identity,
   name                 nvarchar(32)         not null,
   code                 nvarchar(32)         not null,
   icon                 nvarchar(64)         null,
   sort                 int                  null,
   created_by           int                  not null,
   created_time         datetime             not null,
   last_updated_by      int                  null,
   last_updated_time    datetime             null,
   constraint PK_T_RIGHTS_BUTTON primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_button') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_button' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '��ť(����)��', 
   'user', @CurrentUser, 'table', 't_rights_button'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ť����',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'code')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'code'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ť����',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'code'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'icon')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'icon'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ťͼ��',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'icon'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'sort')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'sort'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'sort'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'created_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������id',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'created_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'created_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'created_time'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'last_updated_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������id',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'last_updated_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'last_updated_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������ʱ��',
   'user', @CurrentUser, 'table', 't_rights_button', 'column', 'last_updated_time'
go

/*==============================================================*/
/* Table: t_rights_menu                                         */
/*==============================================================*/
create table t_rights_menu (
   id                   int                  identity,
   name                 nvarchar(64)         not null,
   parent_id            int                  not null,
   code                 nvarchar(32)         null,
   url                  nvarchar(256)        null,
   icon                 nvarchar(128)        null,
   sort                 int                  null,
   created_by           int                  not null,
   created_time         datetime             not null,
   last_updated_by      int                  null,
   last_updated_time    datetime             null,
   constraint PK_T_RIGHTS_MENU primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_menu') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_menu' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '�˵���', 
   'user', @CurrentUser, 'table', 't_rights_menu'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�˵�����',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'parent_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'parent_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ϼ��˵�id',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'parent_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'code')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'code'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�˵�����',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'code'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'url')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'url'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�˵�url',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'url'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'icon')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'icon'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�˵�ͼ��',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'icon'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'sort')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'sort'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�˵�����',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'sort'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'created_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������id',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'created_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'created_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'created_time'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'last_updated_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������id',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'last_updated_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'last_updated_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������ʱ��',
   'user', @CurrentUser, 'table', 't_rights_menu', 'column', 'last_updated_time'
go

/*==============================================================*/
/* Table: t_rights_menu_button                                  */
/*==============================================================*/
create table t_rights_menu_button (
   id                   int                  identity,
   menu_id              int                  null,
   button_id            int                  null,
   constraint PK_T_RIGHTS_MENU_BUTTON primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_menu_button') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_menu_button' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '�˵�-��ť��', 
   'user', @CurrentUser, 'table', 't_rights_menu_button'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'menu_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu_button', 'column', 'menu_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�˵�id',
   'user', @CurrentUser, 'table', 't_rights_menu_button', 'column', 'menu_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_menu_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'button_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_menu_button', 'column', 'button_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ťid',
   'user', @CurrentUser, 'table', 't_rights_menu_button', 'column', 'button_id'
go

/*==============================================================*/
/* Table: t_rights_organization                                 */
/*==============================================================*/
create table t_rights_organization (
   id                   int                  identity,
   name                 nvarchar(128)        not null,
   parent_id            int                  not null,
   code                 nvarchar(512)        null,
   organization_type    tinyint              null,
   sort                 int                  null,
   enable_flag          bit                  null,
   created_by           int                  not null,
   created_time         datetime             not null,
   last_updated_by      int                  null,
   last_updated_time    datetime             null,
   constraint PK_T_RIGHTS_ORGANIZATION primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_organization') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_organization' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '��֯���������νṹ���ݡ�', 
   'user', @CurrentUser, 'table', 't_rights_organization'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'parent_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'parent_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ϼ�����id',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'parent_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'code')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'code'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'code'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'organization_type')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'organization_type'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�������ͣ��ܹ�˾���ֹ�˾�����ŵȡ�',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'organization_type'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'sort')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'sort'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'sort'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'enable_flag')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'enable_flag'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ����ñ�ʶ��Y��ʾ����N��ʾ���ã�Ĭ�����á�',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'enable_flag'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'created_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������id',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'created_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'created_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'created_time'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'last_updated_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����޸���id',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'last_updated_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'last_updated_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����޸�ʱ��',
   'user', @CurrentUser, 'table', 't_rights_organization', 'column', 'last_updated_time'
go

/*==============================================================*/
/* Table: t_rights_role                                         */
/*==============================================================*/
create table t_rights_role (
   id                   int                  identity,
   name                 nvarchar(64)         not null,
   description          nvarchar(246)        null,
   organization_id      int                  not null,
   created_by           int                  not null,
   created_time         datetime             not null,
   last_updated_by      int                  null,
   last_updated_time    datetime             null,
   constraint PK_T_RIGHTS_ROLE primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_role') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_role' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '��ɫ��', 
   'user', @CurrentUser, 'table', 't_rights_role'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ɫ����',
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'description')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'description'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ɫ����',
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'description'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'organization_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'organization_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������id����ɫ�����������ġ�',
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'organization_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'created_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������id',
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'created_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'created_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'created_time'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'last_updated_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������id',
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'last_updated_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'last_updated_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������ʱ��',
   'user', @CurrentUser, 'table', 't_rights_role', 'column', 'last_updated_time'
go

/*==============================================================*/
/* Table: t_rights_role_menu_button                             */
/*==============================================================*/
create table t_rights_role_menu_button (
   id                   int                  identity,
   role_id              int                  null,
   menu_id              int                  null,
   button_id            int                  null,
   constraint PK_T_RIGHTS_ROLE_MENU_BUTTON primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_role_menu_button') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_role_menu_button' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '��ɫ-�˵�-��ť', 
   'user', @CurrentUser, 'table', 't_rights_role_menu_button'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role_menu_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'role_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role_menu_button', 'column', 'role_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ɫid',
   'user', @CurrentUser, 'table', 't_rights_role_menu_button', 'column', 'role_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role_menu_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'menu_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role_menu_button', 'column', 'menu_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�˵�id',
   'user', @CurrentUser, 'table', 't_rights_role_menu_button', 'column', 'menu_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_role_menu_button')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'button_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_role_menu_button', 'column', 'button_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ťid',
   'user', @CurrentUser, 'table', 't_rights_role_menu_button', 'column', 'button_id'
go

/*==============================================================*/
/* Table: t_rights_user                                         */
/*==============================================================*/
create table t_rights_user (
   id                   int                  identity,
   user_id              nvarchar(64)         not null,
   password             nvarchar(64)         not null,
   user_name            nvarchar(64)         not null,
   is_change_pwd        bit                  null,
   enable_flag          bit                  null,
   created_by           int                  not null,
   created_time         datetime             not null,
   last_updated_by      int                  null,
   last_updated_time    datetime             null,
   constraint PK_T_RIGHTS_USER primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_user') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_user' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '�û���', 
   'user', @CurrentUser, 'table', 't_rights_user'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'user_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'user_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û�id',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'user_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'password')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'password'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'password'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'user_name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'user_name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û���',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'user_name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'is_change_pwd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'is_change_pwd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ��״ε�½����',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'is_change_pwd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'enable_flag')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'enable_flag'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ����ñ�ʶ',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'enable_flag'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'created_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������id',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'created_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'created_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'created_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'created_time'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_by')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'last_updated_by'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����޸���id',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'last_updated_by'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_updated_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'last_updated_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����޸�ʱ��',
   'user', @CurrentUser, 'table', 't_rights_user', 'column', 'last_updated_time'
go

/*==============================================================*/
/* Table: t_rights_user_organization                            */
/*==============================================================*/
create table t_rights_user_organization (
   id                   int                  identity,
   user_id              int                  null,
   organization_id      int                  null,
   constraint PK_T_RIGHTS_USER_ORGANIZATION primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_user_organization') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_user_organization' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '�û�-������', 
   'user', @CurrentUser, 'table', 't_rights_user_organization'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user_organization', 'column', 'id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 't_rights_user_organization', 'column', 'id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'user_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user_organization', 'column', 'user_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û�id',
   'user', @CurrentUser, 'table', 't_rights_user_organization', 'column', 'user_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user_organization')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'organization_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user_organization', 'column', 'organization_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����id',
   'user', @CurrentUser, 'table', 't_rights_user_organization', 'column', 'organization_id'
go

/*==============================================================*/
/* Table: t_rights_user_role                                    */
/*==============================================================*/
create table t_rights_user_role (
   id                   int                  identity,
   user_id              int                  null,
   role_id              int                  null,
   constraint PK_T_RIGHTS_USER_ROLE primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_rights_user_role') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_rights_user_role' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '�û�-��ɫ��', 
   'user', @CurrentUser, 'table', 't_rights_user_role'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user_role', 'column', 'id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 't_rights_user_role', 'column', 'id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'user_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user_role', 'column', 'user_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û�id',
   'user', @CurrentUser, 'table', 't_rights_user_role', 'column', 'user_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_rights_user_role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'role_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_rights_user_role', 'column', 'role_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ɫid',
   'user', @CurrentUser, 'table', 't_rights_user_role', 'column', 'role_id'
go

