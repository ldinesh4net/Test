using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Text;
using KPMGTest.Model;
using WebKPMG.Models;
using KPMGTest.Interface;
using KPMGTest.Data;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.OleDb;




namespace WebKPMG.Controllers
{
    public class TaxInfoController : Controller
    {
        public readonly ITaxInfoRepository _Taxinfo;
        DataTable dt = new DataTable();
      


        public TaxInfoController(ITaxInfoRepository Taxinfo)
        {
            this._Taxinfo = Taxinfo;
        }

    
        // GET: TaxInfo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file_Uploader)
        {
           
            List<ErrorlistViewModel> error = new List<ErrorlistViewModel>();
            List<TaxInfoViewModel> taxinfo = new List<TaxInfoViewModel>();
            
            if (file_Uploader != null)
            {

                string fileName = string.Empty;
                string destinationPath = string.Empty;
             
                string connString = "";
             
                fileName = Path.GetFileName(file_Uploader.FileName);

                destinationPath = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                file_Uploader.SaveAs(destinationPath);

                //Start file processing ere

                //IF file tye CSV
                if (file_Uploader.FileName.EndsWith(".csv"))
                {
 
                    using (StreamReader sr = new StreamReader(destinationPath))
                    {
                        
                        int rownum = 1;
                        string errortype="";
                        

                        while (!sr.EndOfStream)
                        {
                           
                            string[] rows = sr.ReadLine().Split(',');
                           
                            ErrorlistViewModel er = new ErrorlistViewModel();
                            TaxInfoViewModel info = new TaxInfoViewModel();

                           //DO Validate

                            errortype = _Taxinfo.Dovalidate(rows[0].ToString(), rows[1].ToString(), rows[2].ToString(), rows[3].ToString());
                            
                              
                                if (errortype != "")
                               {
                                   er.row = rownum;
                                   er.Error = errortype;
                                   error.Add(er);

                                   
                               }
                               
                               if(error.Count==0)
                               {
                                  

                                   info.Account = rows[0];
                                   info.Description = rows[1];
                                   info.Currency = rows[2];
                                   info.Amount = Convert.ToDecimal(rows[3]);

                                   taxinfo.Add(info);
                                   
                               }

                               errortype = "";
                               rownum = rownum + 1;
                        
                        }


                        if (error.Count>0)
                        {
                            ViewBag.Message = "Please Correct following errors!! and Re Upload.";
                            return View(error);
                        }
                        else
                        {

                            for (var i = 0; i < taxinfo.Count; i++)
                            {
                                 var details = new Tax_Information

                                 {
                                     Account = taxinfo[i].Account,
                                     Description = taxinfo[i].Description,
                                     Currency = taxinfo[i].Currency,
                                     Amount = Convert.ToDecimal(taxinfo[i].Amount),

                                 };
                  
                                    _Taxinfo.Save(details);

                            }

                                ViewBag.Message = "Data Saved Successfully.";
                            
                        }
                 

                        
                    }
                  
                }

                //IF file tye Xls
                else if (file_Uploader.FileName.EndsWith(".xls"))
                {
                    string errortype = "";
                    ErrorlistViewModel er = new ErrorlistViewModel();
                    TaxInfoViewModel info = new TaxInfoViewModel();
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destinationPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                    dt = _Taxinfo.ConvertXSLXtoDataTable(destinationPath, connString);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //DO Validate

                        errortype = _Taxinfo.Dovalidate(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());

                        if (errortype != "")
                        {
                            er.row = i;
                            er.Error = errortype;
                            error.Add(er);


                        }

                        if (error.Count == 0)
                        {


                            info.Account = dt.Rows[i][0].ToString();
                            info.Description = dt.Rows[i][1].ToString();
                            info.Currency = dt.Rows[i][2].ToString();
                            info.Amount = Convert.ToDecimal(dt.Rows[i][3].ToString());

                            taxinfo.Add(info);

                        }
                            
                    }


                    if (error.Count > 0)
                    {
                        ViewBag.Message = "Please Correct following errors!! and Re Upload.";
                        return View(error);
                    }
                    else
                    {

                        for (var i = 0; i < taxinfo.Count; i++)
                        {
                            var details = new Tax_Information
                            {
                                Account = taxinfo[i].Account,
                                Description = taxinfo[i].Description,
                                Currency = taxinfo[i].Currency,
                                Amount = Convert.ToDecimal(taxinfo[i].Amount),

                            };
                            _Taxinfo.Save(details);

                        }

                        ViewBag.Message = "Data Saved Successfully.";

                    }
                 

                        
               
                 
                }
                //IF file tye Xlsx
                else if (file_Uploader.FileName.EndsWith(".xlsx"))
                {
                    string errortype = "";
                    ErrorlistViewModel er = new ErrorlistViewModel();
                    TaxInfoViewModel info = new TaxInfoViewModel();
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                    dt = _Taxinfo.ConvertXSLXtoDataTable(destinationPath, connString);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //DO Validate

                        errortype = _Taxinfo.Dovalidate(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());

                        if (errortype != "")
                        {
                            er.row = i+1;
                            er.Error = errortype;
                            error.Add(er);


                        }

                        if (error.Count == 0)
                        {


                            info.Account = dt.Rows[i][0].ToString();
                            info.Description = dt.Rows[i][1].ToString();
                            info.Currency = dt.Rows[i][2].ToString();
                            info.Amount = Convert.ToDecimal(dt.Rows[i][3].ToString());

                            taxinfo.Add(info);

                        }

                    }


                    if (error.Count > 0)
                    {
                        ViewBag.Message = "Please Correct following errors!! and Re Upload.";
                        return View(error);
                    }
                    else
                    {

                        for (var i = 0; i < taxinfo.Count; i++)
                        {
                            var details = new Tax_Information
                            {
                                Account = taxinfo[i].Account,
                                Description = taxinfo[i].Description,
                                Currency = taxinfo[i].Currency,
                                Amount = Convert.ToDecimal(taxinfo[i].Amount),

                            };
                            _Taxinfo.Save(details);

                        }

                        ViewBag.Message = "Data Saved Successfully.";

                    }
                 

                }  
                else
                {
                    ViewBag.Message = "Please Upload Files in .xls, .xlsx or .csv format";

                }
 

                return View();

            }
            return View();

        }


       
     
    }
}