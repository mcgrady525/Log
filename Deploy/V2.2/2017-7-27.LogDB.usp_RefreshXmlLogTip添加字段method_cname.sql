USE [LogDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_RefreshXmlLogTip]    Script Date: 07/27/2017 10:57:25 ******/
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'[dbo].[usp_RefreshXmlLogTip]')
                    AND type IN ( N'P', N'PC' ) ) 
    DROP PROCEDURE [dbo].[usp_RefreshXmlLogTip]
GO

USE [LogDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_RefreshXmlLogTip]    Script Date: 07/27/2017 10:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[usp_RefreshXmlLogTip]
AS 
    BEGIN
        DECLARE @CurrentTime AS DATETIME= GETDATE() ,
            @ReturnVal AS INT= 0;

        BEGIN TRAN;
        BEGIN TRY;
			--先删除，后添加
            TRUNCATE TABLE dbo.t_logs_xml_log_tip;
			
			--添加
            INSERT  INTO dbo.t_logs_xml_log_tip
                    ( system_code ,
                      source ,
                      class_name ,
                      method_name ,
                      method_cname,
                      created_time ,
                      modified_time
	                )
                    SELECT  xmlLogs.system_code ,
                            xmlLogs.source ,
                            xmlLogs.class_name ,
                            xmlLogs.method_name ,
                            xmlLogs.method_cname ,
                            @CurrentTime ,
                            @CurrentTime
                    FROM    dbo.t_logs_xml_log AS xmlLogs
                    WHERE   1 = 1
                            AND xmlLogs.system_code IS NOT NULL
                            AND xmlLogs.system_code != ''
                            AND xmlLogs.source IS NOT NULL
                            AND xmlLogs.source != ''
                    GROUP BY xmlLogs.system_code ,
                            xmlLogs.source ,
                            xmlLogs.class_name ,
                            xmlLogs.method_name,
                            xmlLogs.method_cname
                    ORDER BY xmlLogs.system_code ,
                            xmlLogs.source ,
                            xmlLogs.class_name ,
                            xmlLogs.method_name,
                            xmlLogs.method_cname;
            COMMIT TRAN;
            SET @ReturnVal = 1;
        END TRY
	
        BEGIN CATCH
            ROLLBACK;
        END CATCH	
        
        RETURN @ReturnVal;
    END

GO


