using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using ERP.DAL;

namespace ERP.Model
{
    public class ACC_COAModel
    {
        public Int64 ACC_COA_ID { get; set; }




        public List<ACC_COAModel> CoaDataList()
        {
            const string sp = "pkg_leave.hr_leave_type_select";
            try
            {
                var obList = new List<ACC_COAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    //new CommandParameter() {ParameterName = "pACC_COA_ID", Value = pACC_COA_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_COAModel ob = new ACC_COAModel();
                    ob.ACC_COA_ID = (dr["ACC_COA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_COA_ID"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
