using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KPMGTest.Model;
using System.Data;
namespace KPMGTest.Interface
{
    public interface ITaxInfoRepository
    {
        IQueryable<Tax_Information> TaxinfoQuery { get; }
        void Save(Tax_Information Taxinfo_Model);
        void HardRemove(Tax_Information Taxinfo_Model);
        bool Delete(int Id);

        bool TryGetCurrencySymbol(string ISOCurrencySymbol, out string symbol);

        DataTable ConvertXSLXtoDataTable(string strFilePath, string connString);

        string Dovalidate(string account,string description,string currency,string amount);
    
    }
}
