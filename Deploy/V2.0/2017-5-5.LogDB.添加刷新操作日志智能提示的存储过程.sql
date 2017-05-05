USE [LogDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_RefreshDebugLogTip]    Script Date: 05/05/2017 22:12:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RefreshOperateLogTip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_RefreshOperateLogTip]
GO

USE [LogDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_RefreshOperateLogTip]    Script Date: 05/05/2017 22:12:16 ******/
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


