USE LogDB;
GO


--debug log
SELECT * FROM dbo.t_logs_debug_log;

--插入
INSERT INTO dbo.t_logs_debug_log VALUES ( @SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@Message ,@Detail ,@CreatedTime);