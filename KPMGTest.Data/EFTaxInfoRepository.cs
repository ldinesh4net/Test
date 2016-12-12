using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMGTest.Interface;
using KPMGTest.Model;
using KPMGTest.Data;
using System.Data.Entity;
using System.Globalization;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;

namespace KPMGTest.Data
{
    public class EFTaxInfoRepository:ITaxInfoRepository
    {

        #region Fields
        public EFDbContext context = new EFDbContext();
        #endregion


        #region Read
        public IQueryable<Tax_Information> TaxinfoQuery
        {
            get { return context.Tax_Information; }
        }
        #endregion
        #region Create & Update & Delete
        public void HardRemove(Tax_Information Taxinfo_Model)
        {
            //string UserId = countries.CountryId;
            Tax_Information dbEntry = context.Tax_Information.Find(Taxinfo_Model.TaxId);

            if (dbEntry != null)
            {
                context.Tax_Information.Remove(Taxinfo_Model);
                context.SaveChanges();
                
            }
        }

        public bool Delete(int Id)
        {
            Tax_Information dbEntry = context.Tax_Information.Find(Id);
            if (dbEntry != null)
            {
                context.Tax_Information.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Save(Tax_Information Taxinfo_Model)
        {
            string Action = string.Empty;
            // Add new
            if (Taxinfo_Model.TaxId == 0)
            {
                context.Tax_Information.Add(Taxinfo_Model);
                context.SaveChanges();
                
            }
            else
            {
                Tax_Information dbEntry = context.Tax_Information.Find(Taxinfo_Model.TaxId);
                if (dbEntry != null)
                {

                    dbEntry.Account = Taxinfo_Model.Account;
                    dbEntry.Amount = Taxinfo_Model.Amount;
                    dbEntry.Currency = Taxinfo_Model.Currency;
                    dbEntry.Description = Taxinfo_Model.Description;
                    context.SaveChanges();
                  
                }
            }
        }
        #endregion




        public bool TryGetCurrencySymbol(string ISOCurrencySymbol, out string symbol)
        {
            symbol = CultureInfo
               .GetCultures(CultureTypes.AllCultures)
               .Where(c => !c.IsNeutralCulture)
               .Select(culture =>
               {
                   try
                   {
                       return new RegionInfo(culture.LCID);
                   }
                   catch
                   {
                       return null;
                   }
               })
               .Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol)
               .Select(ri => ri.ISOCurrencySymbol)
               .FirstOrDefault();
            return symbol != null;
        }

        public System.Data.DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                oleda.Fill(ds);

                dt = ds.Tables[0];
          

            }
            catch
            {
            }
            finally
            {

                oledbConn.Close();

            }
            return dt;
        }


        public string Dovalidate(string account, string description, string currency, string amount)
        {
            string errortype = "";
            if (account == "")
            {

                errortype = "Account number Can not be blank.";
            }

            if (description == "")
            {
                errortype = errortype + "\n" + "Description Can not be blank.";

            }

            if (currency == "")
            {
                errortype = errortype + "\n" + "Currency code Can not be blank.";

            }
            else
            {
                //Validate Currency Code
                string Currencycode;
               TryGetCurrencySymbol(currency, out Currencycode);

               if (Currencycode != currency)
                {
                    errortype = errortype + "\n" + "Currency code must be in ISO 4217 format.";
                }
            }

            if (amount == "")
            {
                errortype = errortype + "\n" + "Amount Can not be blank.";
            }
            else
            {
                var isValidNumber = Regex.IsMatch(amount, @"^[0-9]+(\.[0-9]+)?$");

                if (isValidNumber == false)
                {
                    errortype = errortype + "\n" + "invalid Amount.";
                }
            }

            return errortype;
        }
    }
}
