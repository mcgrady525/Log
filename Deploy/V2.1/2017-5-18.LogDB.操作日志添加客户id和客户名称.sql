USE LogDB;
GO

--操作日志添加客户id和客户名称
--corp_id，客户id
--corp_name，客户名称
IF COL_LENGTH('dbo.t_logs_operate_log', 'corp_id') IS NULL
	ALTER TABLE dbo.t_logs_operate_log ADD corp_id BIGINT NULL;
go

IF COL_LENGTH('dbo.t_logs_operate_log', 'corp_name') IS NULL
	ALTER TABLE dbo.t_logs_operate_log ADD corp_name NVARCHAR(128) NULL;
go
	
