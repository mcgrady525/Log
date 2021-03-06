USE [LogDB]
GO
/****** Object:  Table [dbo].[t_rights_user_role]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_user_role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_T_RIGHTS_USER_ROLE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user_role', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user_role', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user_role', @level2type=N'COLUMN',@level2name=N'role_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户-角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user_role'
GO
/****** Object:  Table [dbo].[t_rights_user_organization]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_user_organization](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[organization_id] [int] NULL,
 CONSTRAINT [PK_T_RIGHTS_USER_ORGANIZATION] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user_organization', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user_organization', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user_organization', @level2type=N'COLUMN',@level2name=N'organization_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户-机构表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user_organization'
GO
/****** Object:  Table [dbo].[t_rights_user]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [nvarchar](64) NOT NULL,
	[password] [nvarchar](64) NOT NULL,
	[user_name] [nvarchar](64) NOT NULL,
	[is_change_pwd] [bit] NULL,
	[enable_flag] [bit] NULL,
	[created_by] [int] NOT NULL,
	[created_time] [datetime] NOT NULL,
	[last_updated_by] [int] NULL,
	[last_updated_time] [datetime] NULL,
 CONSTRAINT [PK_T_RIGHTS_USER] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'user_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否首次登陆改密' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'is_change_pwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'enable_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'created_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'created_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'last_updated_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user', @level2type=N'COLUMN',@level2name=N'last_updated_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_user'
GO
/****** Object:  Table [dbo].[t_rights_role_menu_button]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_role_menu_button](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NULL,
	[menu_id] [int] NULL,
	[button_id] [int] NULL,
 CONSTRAINT [PK_T_RIGHTS_ROLE_MENU_BUTTON] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role_menu_button', @level2type=N'COLUMN',@level2name=N'role_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role_menu_button', @level2type=N'COLUMN',@level2name=N'menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role_menu_button', @level2type=N'COLUMN',@level2name=N'button_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色-菜单-按钮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role_menu_button'
GO
/****** Object:  Table [dbo].[t_rights_role]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[description] [nvarchar](246) NULL,
	[organization_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[created_time] [datetime] NOT NULL,
	[last_updated_by] [int] NULL,
	[last_updated_time] [datetime] NULL,
 CONSTRAINT [PK_T_RIGHTS_ROLE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属机构id，角色有所属机构的。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role', @level2type=N'COLUMN',@level2name=N'organization_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role', @level2type=N'COLUMN',@level2name=N'created_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role', @level2type=N'COLUMN',@level2name=N'created_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role', @level2type=N'COLUMN',@level2name=N'last_updated_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role', @level2type=N'COLUMN',@level2name=N'last_updated_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_role'
GO
/****** Object:  Table [dbo].[t_rights_organization]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_organization](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](128) NOT NULL,
	[parent_id] [int] NOT NULL,
	[code] [nvarchar](512) NULL,
	[organization_type] [tinyint] NULL,
	[sort] [int] NULL,
	[enable_flag] [bit] NULL,
	[created_by] [int] NOT NULL,
	[created_time] [datetime] NOT NULL,
	[last_updated_by] [int] NULL,
	[last_updated_time] [datetime] NULL,
 CONSTRAINT [PK_T_RIGHTS_ORGANIZATION] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级机构id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'parent_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构类型，总公司，分公司，部门等。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'organization_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用标识，Y表示启用N表示禁用，默认启用。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'enable_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'created_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'created_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'last_updated_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization', @level2type=N'COLUMN',@level2name=N'last_updated_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构表，树形结构数据。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_organization'
GO
/****** Object:  Table [dbo].[t_rights_menu_button]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_menu_button](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[menu_id] [int] NULL,
	[button_id] [int] NULL,
 CONSTRAINT [PK_T_RIGHTS_MENU_BUTTON] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu_button', @level2type=N'COLUMN',@level2name=N'menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu_button', @level2type=N'COLUMN',@level2name=N'button_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单-按钮表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu_button'
GO
/****** Object:  Table [dbo].[t_rights_menu]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_menu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[parent_id] [int] NOT NULL,
	[code] [nvarchar](32) NULL,
	[url] [nvarchar](256) NULL,
	[icon] [nvarchar](128) NULL,
	[sort] [int] NULL,
	[created_by] [int] NOT NULL,
	[created_time] [datetime] NOT NULL,
	[last_updated_by] [int] NULL,
	[last_updated_time] [datetime] NULL,
 CONSTRAINT [PK_T_RIGHTS_MENU] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级菜单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'parent_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'created_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'created_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'last_updated_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu', @level2type=N'COLUMN',@level2name=N'last_updated_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_menu'
GO
/****** Object:  Table [dbo].[t_rights_button]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_rights_button](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](32) NOT NULL,
	[code] [nvarchar](32) NOT NULL,
	[icon] [nvarchar](64) NULL,
	[sort] [int] NULL,
	[created_by] [int] NOT NULL,
	[created_time] [datetime] NOT NULL,
	[last_updated_by] [int] NULL,
	[last_updated_time] [datetime] NULL,
 CONSTRAINT [PK_T_RIGHTS_BUTTON] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'created_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'created_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'last_updated_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button', @level2type=N'COLUMN',@level2name=N'last_updated_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮(动作)表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_rights_button'
GO
/****** Object:  Table [dbo].[t_logs_xml_log_tip]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_logs_xml_log_tip](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[class_name] [nvarchar](64) NULL,
	[method_name] [nvarchar](64) NULL,
	[created_time] [datetime] NULL,
	[modified_time] [datetime] NULL,
 CONSTRAINT [PK_T_LOGS_XML_LOG_TIP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'xml日志智能提示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_xml_log_tip'
GO
/****** Object:  Table [dbo].[t_logs_xml_log]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_logs_xml_log](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[machine_name] [nvarchar](64) NULL,
	[ip_address] [nvarchar](512) NULL,
	[process_id] [int] NULL,
	[process_name] [nvarchar](128) NULL,
	[thread_id] [int] NULL,
	[thread_name] [nvarchar](128) NULL,
	[appdomain_name] [nvarchar](512) NULL,
	[class_name] [nvarchar](64) NULL,
	[method_name] [nvarchar](64) NULL,
	[remark] [nvarchar](max) NULL,
	[created_time] [datetime] NULL,
	[rq] [varbinary](max) NULL,
	[rs] [varbinary](max) NULL,
	[client_ip] [nvarchar](512) NULL,
	[method_cname] [nvarchar](64) NULL,
 CONSTRAINT [PK_T_LOGS_XML_LOG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'xml日志(记录接口请求和响应结果)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_xml_log'
GO
/****** Object:  Table [dbo].[t_logs_performance_log_tip]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_logs_performance_log_tip](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[class_name] [nvarchar](64) NULL,
	[method_name] [nvarchar](64) NULL,
	[created_time] [datetime] NULL,
	[modified_time] [datetime] NULL,
 CONSTRAINT [PK_T_LOGS_PERFORMANCE_LOG_TIP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性能日志智能提示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_performance_log_tip'
GO
/****** Object:  Table [dbo].[t_logs_performance_log]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_logs_performance_log](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[machine_name] [nvarchar](64) NULL,
	[ip_address] [nvarchar](512) NULL,
	[process_id] [int] NULL,
	[process_name] [nvarchar](128) NULL,
	[thread_id] [int] NULL,
	[thread_name] [nvarchar](128) NULL,
	[class_name] [nvarchar](64) NULL,
	[method_name] [nvarchar](64) NULL,
	[duration] [bigint] NULL,
	[remark] [nvarchar](max) NULL,
	[created_time] [datetime] NULL,
	[client_ip] [nvarchar](512) NULL,
	[method_cname] [nvarchar](64) NULL,
 CONSTRAINT [PK_T_LOGS_PERFORMANCE_LOG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性能日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_performance_log'
GO
/****** Object:  Table [dbo].[t_logs_operate_log_tip]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_logs_operate_log_tip](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[operate_module] [nvarchar](64) NULL,
	[operate_type] [nvarchar](32) NULL,
	[created_time] [datetime] NULL,
	[modified_time] [datetime] NULL,
 CONSTRAINT [PK_T_LOGS_OPERATE_LOG_TIP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作日志智能提示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_operate_log_tip'
GO
/****** Object:  Table [dbo].[t_logs_operate_log]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_logs_operate_log](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[machine_name] [nvarchar](64) NULL,
	[ip_address] [nvarchar](512) NULL,
	[process_id] [int] NULL,
	[process_name] [nvarchar](128) NULL,
	[thread_id] [int] NULL,
	[thread_name] [nvarchar](128) NULL,
	[appdomain_name] [nvarchar](512) NULL,
	[operated_time] [datetime] NULL,
	[user_id] [nvarchar](32) NULL,
	[user_name] [nvarchar](32) NULL,
	[operate_module] [nvarchar](64) NULL,
	[operate_type] [nvarchar](32) NULL,
	[modify_before] [varbinary](max) NULL,
	[modify_after] [varbinary](max) NULL,
	[created_time] [datetime] NULL,
	[client_ip] [nvarchar](512) NULL,
 CONSTRAINT [PK_T_LOGS_OPERATE_LOG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_operate_log'
GO
/****** Object:  Table [dbo].[t_logs_error_log_tip]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_logs_error_log_tip](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[created_time] [datetime] NULL,
	[modified_time] [datetime] NULL,
 CONSTRAINT [PK_T_LOGS_ERROR_LOG_TIP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'错误日志智能提示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_error_log_tip'
GO
/****** Object:  Table [dbo].[t_logs_error_log_black_list]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_logs_error_log_black_list](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[machine_name] [nvarchar](64) NULL,
	[ip_address] [nvarchar](512) NULL,
	[client_ip] [nvarchar](512) NULL,
	[appdomain_name] [nvarchar](512) NULL,
	[message] [nvarchar](1024) NULL,
	[is_regex] [bit] NULL,
	[created_by] [int] NOT NULL,
	[created_time] [datetime] NOT NULL,
	[last_updated_by] [int] NULL,
	[last_updated_time] [datetime] NULL,
 CONSTRAINT [PK_T_LOGS_ERROR_LOG_BLACK_LIST] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_error_log_black_list', @level2type=N'COLUMN',@level2name=N'created_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_error_log_black_list', @level2type=N'COLUMN',@level2name=N'created_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_error_log_black_list', @level2type=N'COLUMN',@level2name=N'last_updated_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_error_log_black_list', @level2type=N'COLUMN',@level2name=N'last_updated_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'错误日志黑名单。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_error_log_black_list'
GO
/****** Object:  Table [dbo].[t_logs_error_log]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_logs_error_log](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[machine_name] [nvarchar](64) NULL,
	[ip_address] [nvarchar](512) NULL,
	[process_id] [int] NULL,
	[process_name] [nvarchar](128) NULL,
	[thread_id] [int] NULL,
	[thread_name] [nvarchar](128) NULL,
	[appdomain_name] [nvarchar](512) NULL,
	[created_time] [datetime] NULL,
	[detail] [varbinary](max) NULL,
	[message] [varbinary](max) NULL,
	[client_ip] [nvarchar](512) NULL,
 CONSTRAINT [PK_T_LOGS_ERROR_LOG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'错误日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_error_log'
GO
/****** Object:  Table [dbo].[t_logs_debug_log_tip]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_logs_debug_log_tip](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[created_time] [datetime] NULL,
	[modified_time] [datetime] NULL,
 CONSTRAINT [PK_T_LOGS_DEBUG_LOG_TIP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'调试日志智能提示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_debug_log_tip'
GO
/****** Object:  Table [dbo].[t_logs_debug_log_black_list]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_logs_debug_log_black_list](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[machine_name] [nvarchar](64) NULL,
	[ip_address] [nvarchar](512) NULL,
	[client_ip] [nvarchar](512) NULL,
	[appdomain_name] [nvarchar](512) NULL,
	[message] [nvarchar](1024) NULL,
	[is_regex] [bit] NULL,
	[created_by] [int] NOT NULL,
	[created_time] [datetime] NOT NULL,
	[last_updated_by] [int] NULL,
	[last_updated_time] [datetime] NULL,
 CONSTRAINT [PK_T_LOGS_DEBUG_LOG_BLACK_LIST] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_debug_log_black_list', @level2type=N'COLUMN',@level2name=N'created_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_debug_log_black_list', @level2type=N'COLUMN',@level2name=N'created_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_debug_log_black_list', @level2type=N'COLUMN',@level2name=N'last_updated_by'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_debug_log_black_list', @level2type=N'COLUMN',@level2name=N'last_updated_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'调试日志黑名单。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_debug_log_black_list'
GO
/****** Object:  Table [dbo].[t_logs_debug_log]    Script Date: 05/22/2017 12:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_logs_debug_log](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[system_code] [nvarchar](32) NULL,
	[source] [nvarchar](64) NULL,
	[machine_name] [nvarchar](64) NULL,
	[ip_address] [nvarchar](512) NULL,
	[process_id] [int] NULL,
	[process_name] [nvarchar](128) NULL,
	[thread_id] [int] NULL,
	[thread_name] [nvarchar](128) NULL,
	[appdomain_name] [nvarchar](512) NULL,
	[created_time] [datetime] NULL,
	[detail] [varbinary](max) NULL,
	[message] [varbinary](max) NULL,
	[client_ip] [nvarchar](512) NULL,
 CONSTRAINT [PK_T_LOGS_DEBUG_LOG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'调试日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_logs_debug_log'
GO
/****** Object:  StoredProcedure [dbo].[usp_RefreshXmlLogTip]    Script Date: 05/22/2017 12:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_RefreshXmlLogTip]
AS 
    BEGIN
        DECLARE @CurrentTime AS DATETIME= GETDATE(),
				@ReturnVal AS INT= 0;

        BEGIN TRAN;
        BEGIN TRY;
			--先删除，后添加
            TRUNCATE TABLE dbo.t_logs_xml_log_tip;
			
			--添加
            INSERT  INTO dbo.t_logs_xml_log_tip
                    ( system_code ,
                      source ,
                      class_name,
                      method_name,
                      created_time ,
                      modified_time
	                )
                    SELECT  xmlLogs.system_code ,
                            xmlLogs.source ,
                            xmlLogs.class_name,
                            xmlLogs.method_name,
                            @CurrentTime ,
                            @CurrentTime
                    FROM dbo.t_logs_xml_log AS xmlLogs
                    WHERE   xmlLogs.system_code IS NOT NULL
                            AND xmlLogs.system_code != ''
                            AND xmlLogs.source IS NOT NULL
                            AND xmlLogs.source != ''
                    GROUP BY xmlLogs.system_code ,
                            xmlLogs.source,
                            xmlLogs.class_name,
                            xmlLogs.method_name
                    ORDER BY xmlLogs.system_code ,
                            xmlLogs.source,
                            xmlLogs.class_name,
                            xmlLogs.method_name;
            COMMIT TRAN;
            SET @ReturnVal= 1;
        END TRY
	
        BEGIN CATCH
            ROLLBACK;
        END CATCH	
        
        RETURN @ReturnVal;
    END
GO
/****** Object:  StoredProcedure [dbo].[usp_RefreshPerformanceLogTip]    Script Date: 05/22/2017 12:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_RefreshPerformanceLogTip]
AS 
    BEGIN
        DECLARE @CurrentTime AS DATETIME= GETDATE(),
				@ReturnVal AS INT= 0;

        BEGIN TRAN;
        BEGIN TRY;
			--先删除，后添加
            TRUNCATE TABLE dbo.t_logs_performance_log_tip;
			
			--添加
            INSERT  INTO dbo.t_logs_performance_log_tip
                    ( system_code ,
                      source ,
                      class_name,
                      method_name,
                      created_time ,
                      modified_time
	                )
                    SELECT  performanceLogs.system_code ,
                            performanceLogs.source ,
                            performanceLogs.class_name,
                            performanceLogs.method_name,
                            @CurrentTime ,
                            @CurrentTime
                    FROM dbo.t_logs_performance_log AS performanceLogs
                    WHERE   performanceLogs.system_code IS NOT NULL
                            AND performanceLogs.system_code != ''
                            AND performanceLogs.source IS NOT NULL
                            AND performanceLogs.source != ''
                    GROUP BY performanceLogs.system_code ,
                            performanceLogs.source,
                            performanceLogs.class_name,
                            performanceLogs.method_name
                    ORDER BY performanceLogs.system_code ,
                            performanceLogs.source,
                            performanceLogs.class_name,
                            performanceLogs.method_name;
            COMMIT TRAN;
            SET @ReturnVal= 1;
        END TRY
	
        BEGIN CATCH
            ROLLBACK;
        END CATCH	
        
        RETURN @ReturnVal;
    END
GO
/****** Object:  StoredProcedure [dbo].[usp_RefreshOperateLogTip]    Script Date: 05/22/2017 12:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_RefreshOperateLogTip]
AS 
    BEGIN
        DECLARE @CurrentTime AS DATETIME= GETDATE() ,
            @ReturnVal AS INT = 0;

        BEGIN TRAN;
        BEGIN TRY;
			--先删除，后添加
            TRUNCATE TABLE dbo.t_logs_operate_log_tip;
			
			--添加
            INSERT  INTO dbo.t_logs_operate_log_tip
                    ( system_code ,
                      source ,
                      operate_module ,
                      operate_type ,
                      created_time ,
                      modified_time
                    )
                    SELECT  operateLogs.system_code ,
                            operateLogs.source ,
                            operateLogs.operate_module ,
                            operateLogs.operate_type ,
                            @CurrentTime ,
                            @CurrentTime
                    FROM    dbo.t_logs_operate_log (NOLOCK) AS operateLogs
                    WHERE   1 = 1
                            AND operateLogs.system_code IS NOT NULL
                            AND operateLogs.system_code != ''
                            AND operateLogs.source IS NOT NULL
                            AND operateLogs.source != ''
                            AND operateLogs.operate_module IS NOT NULL
                            AND operateLogs.operate_module != ''
                            AND operateLogs.operate_type IS NOT NULL
                            AND operateLogs.operate_type != ''
                    GROUP BY operateLogs.system_code ,
                            operateLogs.source ,
                            operateLogs.operate_module ,
                            operateLogs.operate_type
                    ORDER BY operateLogs.system_code ,
                            operateLogs.source ,
                            operateLogs.operate_module ,
                            operateLogs.operate_type;
	
            COMMIT TRAN;            
            SET @ReturnVal = 1;
        END TRY
	
        BEGIN CATCH
            ROLLBACK;
        END CATCH	
        
        RETURN @ReturnVal;
    END
GO
/****** Object:  StoredProcedure [dbo].[usp_RefreshErrorLogTip]    Script Date: 05/22/2017 12:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_RefreshErrorLogTip]
AS 
    BEGIN
        DECLARE @CurrentTime AS DATETIME= GETDATE(),
				@ReturnVal AS INT= 0;

        BEGIN TRAN;
        BEGIN TRY;
			--先删除，后添加
            TRUNCATE TABLE dbo.t_logs_error_log_tip;
			
			--添加
            INSERT  INTO dbo.t_logs_error_log_tip
                    ( system_code ,
                      source ,
                      created_time ,
                      modified_time
	                )
                    SELECT  errorLogs.system_code ,
                            errorLogs.source ,
                            @CurrentTime ,
                            @CurrentTime
                    FROM dbo.t_logs_error_log AS errorLogs
                    WHERE   errorLogs.system_code IS NOT NULL
                            AND errorLogs.system_code != ''
                            AND errorLogs.source IS NOT NULL
                            AND errorLogs.source != ''
                    GROUP BY errorLogs.system_code ,
                            errorLogs.source
                    ORDER BY errorLogs.system_code ,
                            errorLogs.source;	
            COMMIT TRAN;
            SET @ReturnVal= 1;
        END TRY
	
        BEGIN CATCH
            ROLLBACK;
        END CATCH	
        
        RETURN @ReturnVal;
    END
GO
/****** Object:  StoredProcedure [dbo].[usp_RefreshDebugLogTip]    Script Date: 05/22/2017 12:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_RefreshDebugLogTip]
AS 
    BEGIN
        DECLARE @CurrentTime AS DATETIME= GETDATE(),
				@ReturnVal AS INT = 0;

        BEGIN TRAN;
        BEGIN TRY;
			--先删除，后添加
            TRUNCATE TABLE dbo.t_logs_debug_log_tip;
			
			--添加
            INSERT  INTO dbo.t_logs_debug_log_tip
                    ( system_code ,
                      source ,
                      created_time ,
                      modified_time
	                )
                    SELECT  debugLogs.system_code ,
                            debugLogs.source ,
                            @CurrentTime ,
                            @CurrentTime
                    FROM    dbo.t_logs_debug_log AS debugLogs
                    WHERE   debugLogs.system_code IS NOT NULL
                            AND debugLogs.system_code != ''
                            AND debugLogs.source IS NOT NULL
                            AND debugLogs.source != ''
                    GROUP BY debugLogs.system_code ,
                            debugLogs.source
                    ORDER BY debugLogs.system_code ,
                            debugLogs.source;
	
            COMMIT TRAN;            
            SET @ReturnVal= 1;
        END TRY
	
        BEGIN CATCH
            ROLLBACK;
        END CATCH	
        
        RETURN @ReturnVal;
    END
GO
