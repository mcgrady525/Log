USE LogDB;
GO

--操作日志添加client_ip字段并且修改字段ip_address的长度
IF COL_LENGTH('t_logs_operate_log', 'client_ip') IS NULL
	ALTER TABLE dbo.t_logs_operate_log ADD client_ip NVARCHAR(512) NULL;
GO

IF COL_LENGTH('t_logs_operate_log', 'ip_address') IS NOT NULL
	ALTER TABLE dbo.t_logs_operate_log ALTER COLUMN ip_address NVARCHAR(512);
go