using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPMGTest.Model;
using WebKPMG.Models;
using KPMGTest.Interface;
using KPMGTest.Data;
using System.Data.Entity;
namespace WebKPMG.Controllers
{
    public class TaxDetailsController : Controller
    {
        public readonly ITaxInfoRepository _Taxinfo;


        public TaxDetailsController(ITaxInfoRepository Taxinfo)
        {
            this._Taxinfo = Taxinfo;
        }

        // GET: TaxDetails
         [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Gettaxinfo()
         {
            var list = _Taxinfo.TaxinfoQuery.ToList();
            var jsonData = new
            {
                data = list
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetRowbyId(int Id)
        {
            var success = true;
            var row = _Taxinfo.TaxinfoQuery.Where(x => x.TaxId == Id).FirstOrDefault();
            return Json(new { Success = success, row = row }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save(TaxInfoViewModel model)
        {
            var success = true;
            var details = new Tax_Information
            {
                Account = model.Account,
                Description = model.Description,
                Currency = model.Currency,
                Amount = Convert.ToDecimal(model.Amount),

            };
            if (model.TaxId != 0)
                details.TaxId = model.TaxId;
            _Taxinfo.Save(details);
            return Json(new { Success = success }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var success = true;
            _Taxinfo.Delete(Id);
            return Json(new { Success = success }, JsonRequestBehavior.AllowGet);

        }
      
       
    }
}