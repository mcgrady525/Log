using Log.Entity.Common;
using Log.Entity.ViewModel;
using Log.IService.Rights;
using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Consts;
using Tracy.Frameworks.Common.Extends;

namespace Log.Site.Controllers
{
    /// <summary>
    /// 按钮管理
    /// </summary>
    public class ButtonController : BaseController
    {
        //注入service
        private readonly IRightsButtonService _buttonService;

        public ButtonController(IRightsButtonService buttonService)
        {
            _buttonService = buttonService;
        }

        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有按钮(分页)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPagingButtons(GetPagingButtonsRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingButtonsRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            var rs = _buttonService.GetPagingButtons(request);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateTimeTypeConst.DATETIME) + "}";
            }

            return Content(result);
        }

        [LoginAuthorization]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddButtonRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new AddButtonRequest();
            }

            var rs = _buttonService.AddButton(request, loginInfo);
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
                msg = "新增成功!";
            }
            else
            {
                msg = rs.Message.IsNullOrEmpty() ? "新增失败!" : rs.Message;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditButtonRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new EditButtonRequest();
            }

            var rs = _buttonService.EditButton(request, loginInfo);
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
                msg = "修改成功!";
            }
            else
            {
                msg = rs.Message.IsNullOrEmpty() ? "修改失败!" : rs.Message;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(DeleteButtonRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new DeleteButtonRequest();
            }

            var rs = _buttonService.DeleteButton(request);
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
                msg = "删除成功!";
            }
            else
            {
                msg = rs.Message.IsNullOrEmpty() ? "删除失败!" : rs.Message;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

    }
}