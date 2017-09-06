USE LogDB;
GO

--分别建立存储过程，刷新智能提示
--debug log
--usp_RefreshDebugLogTip
IF OBJECT_ID('usp_RefreshDebugLogTip','P') IS NOT NULL
	DROP PROC usp_RefreshDebugLogTip;
go

CREATE PROC usp_RefreshDebugLogTip
AS
BEGIN
	DECLARE @CurrentTime AS DATETIME= GETDATE();

	--先删除，后添加
	TRUNCATE TABLE dbo.t_logs_debug_log_tip;
	
	INSERT INTO dbo.t_logs_debug_log_tip
	        ( system_code ,
	          source ,
	          created_time ,
	          modified_time
	        )	
	SELECT debugLogs.system_code, debugLogs.source, @CurrentTime, @CurrentTime FROM dbo.t_logs_debug_log AS debugLogs
	WHERE debugLogs.system_code IS NOT NULL
	AND debugLogs.source IS NOT NULL
	GROUP BY debugLogs.system_code, debugLogs.source;
END
GO