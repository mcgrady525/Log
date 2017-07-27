USE [LogDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_RefreshPerformanceLogTip]    Script Date: 07/27/2017 11:26:35 ******/
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'[dbo].[usp_RefreshPerformanceLogTip]')
                    AND type IN ( N'P', N'PC' ) ) 
    DROP PROCEDURE [dbo].[usp_RefreshPerformanceLogTip]
GO

USE [LogDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_RefreshPerformanceLogTip]    Script Date: 07/27/2017 11:26:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[usp_RefreshPerformanceLogTip]
AS 
    BEGIN
        DECLARE @CurrentTime AS DATETIME= GETDATE() ,
            @ReturnVal AS INT= 0;

        BEGIN TRAN;
        BEGIN TRY;
			--先删除，后添加
            TRUNCATE TABLE dbo.t_logs_performance_log_tip;
			
			--添加
            INSERT  INTO dbo.t_logs_performance_log_tip
                    ( system_code ,
                      source ,
                      class_name ,
                      method_name ,
                      method_cname ,
                      created_time ,
                      modified_time
	                )
                    SELECT  performanceLogs.system_code ,
                            performanceLogs.source ,
                            performanceLogs.class_name ,
                            performanceLogs.method_name ,
                            performanceLogs.method_cname ,
                            @CurrentTime ,
                            @CurrentTime
                    FROM    dbo.t_logs_performance_log AS performanceLogs
                    WHERE   performanceLogs.system_code IS NOT NULL
                            AND performanceLogs.system_code != ''
                            AND performanceLogs.source IS NOT NULL
                            AND performanceLogs.source != ''
                    GROUP BY performanceLogs.system_code ,
                            performanceLogs.source ,
                            performanceLogs.class_name ,
                            performanceLogs.method_name ,
                            performanceLogs.method_cname
                    ORDER BY performanceLogs.system_code ,
                            performanceLogs.source ,
                            performanceLogs.class_name ,
                            performanceLogs.method_name ,
                            performanceLogs.method_cname;
            COMMIT TRAN;
            SET @ReturnVal = 1;
        END TRY
	
        BEGIN CATCH
            ROLLBACK;
        END CATCH	
        
        RETURN @ReturnVal;
    END

GO


