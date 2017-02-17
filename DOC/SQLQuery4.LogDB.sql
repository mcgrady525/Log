USE LogDB;
GO


--debug log
SELECT * FROM dbo.t_logs_debug_log AS debugLogs
ORDER BY debugLogs.id DESC;

--插入
--INSERT INTO dbo.t_logs_debug_log VALUES ( @SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@Message ,@Detail ,@CreatedTime);

--error log
SELECT * FROM dbo.t_logs_error_log AS errorLogs
ORDER BY errorLogs.id DESC;

--插入
--INSERT INTO dbo.t_logs_error_log VALUES (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@Message ,@Detail ,@CreatedTime);

--xml log
--class_name, method_name,rq,rs
SELECT * FROM dbo.t_logs_xml_log AS xmlLogs
ORDER BY xmlLogs.id DESC;

--插入
--INSERT INTO dbo.t_logs_xml_log VALUES (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@ClassName ,@MethodName ,@Rq ,@Rs ,@Remark ,@CreatedTime);