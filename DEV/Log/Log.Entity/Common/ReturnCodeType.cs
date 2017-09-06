using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

/*********************************************************
 * 开发人员：鲁宁
 * 创建时间：2014/12/31 13:29:06
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
namespace Log.Entity.Common
{
    /// <summary>
    /// 返回枚举
    /// </summary>
    [DataContract, Serializable]
    public enum ReturnCodeType
    {
        #region 通用 0到100

        /// <summary>
        /// 未定义的结果
        /// </summary>
        [EnumMember]
        Unknown = 0,

        /// <summary>
        /// 成功
        /// </summary>
        [EnumMember]
        Success = 1,

        /// <summary>
        /// 出错
        /// </summary>
        [EnumMember]
        Error = 2,

        /// <summary>
        /// 验证失败
        /// </summary>
        [EnumMember]
        VerifyError = 3,


        /// <summary>
        /// 服务器错误
        /// </summary>
        [EnumMember]
        ServiceError = 4,

        /// <summary>
        /// 接口返回空
        /// </summary>
        [EnumMember]
        InterfaceNull = 5,

        /// <summary>
        /// 接口错误
        /// </summary>
        [EnumMember]
        InterfaceError = 6,

        /// <summary>
        /// 接口返回结果为失败
        /// </summary>
        [EnumMember]
        InterfaceFail = 7,

        /// <summary>
        /// 通用数据库访问错误
        /// </summary>
        [EnumMember]
        DBError = 8,

        /// <summary>
        /// 邮件發送失敗
        /// </summary>
        [EnumMember]
        SendFailure = 9,

        /// <summary>
        /// 失敗
        /// </summary>
        [EnumMember]
        Fail = 10,

        /// <summary>
        /// 没找到此订单
        /// </summary>
        [EnumMember]
        OrderNoFind = 11,

        /// <summary>
        /// 沒有電郵件地址
        /// </summary>
        [EnumMember]
        NoEmail = 12,

        /// <summary>
        /// 已出票
        /// </summary>
        [EnumMember]
        HasTicket = 13,

        /// <summary>
        /// 訂單狀態已修改
        /// </summary>
        [EnumMember]
        OrderStatusHasModify = 14,

        /// <summary>
        /// 訂單已存在這個PNR(非本訂單)
        /// </summary>
        OrderExistPNR = 15,

        /// <summary>
        /// 重複訂單
        /// </summary>
        [EnumMember]
        OrderExist = 16,

        /// <summary>
        /// 没有电话号码
        /// </summary>
        [EnumMember]
        NoMobilePhone = 17,

        #endregion

        #region 審批邏輯狀態 101 +

        /// <summary>
        /// 有待提交的審批
        /// </summary>
        [EnumMember]
        WaitSubmit = 101,

        /// <summary>
        /// 有待他人提交的審批
        /// </summary>
        [EnumMember]
        WaitOtherSubmit = 102,

        /// <summary>
        /// 待審批
        /// </summary>
        [EnumMember]
        WaitAudit = 103,

        /// <summary>
        /// 待生效
        /// </summary>
        [EnumMember]
        WaitApplied = 104,

        #endregion

        #region 外部接口150到

        /// <summary>
        /// 邮件接口错误
        /// </summary>
        [EnumMember]
        MailError = 150,

        /// <summary>
        /// Exchange接口错误
        /// </summary>
        [EnumMember]
        ExchangeError = 151,

        /// <summary>
        /// 会员接口错误
        /// </summary>
        [EnumMember]
        MemberInterfaceError = 152,

        /// <summary>
        /// 會員 登入名稱已經存在
        /// </summary>
        [EnumMember]
        UserNameExists = 153,

        /// <summary>
        /// 會員 英文姓氏+英文名字+手提電話+電郵地址 已經存在
        /// </summary>
        [EnumMember]
        UserAccountExists = 154,

        #endregion
    }
}
