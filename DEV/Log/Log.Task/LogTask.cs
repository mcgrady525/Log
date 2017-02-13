using Log.Common;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Log.Task
{
    public partial class LogTask : ServiceBase
    {
        public LogTask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {

        }

        /// <summary>
        /// 停止
        /// </summary>
        protected override void OnStop()
        {

        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLogs(string msg)
        {
            msg = string.Format("【{0}】{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), msg);
            var path = AppDomain.CurrentDomain.BaseDirectory + "LogTask.log";
            var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(msg);

            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
