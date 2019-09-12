using ERP.DAL;
using ERP.Model;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
   public class VoucherTypeRepository: IVoucherTypeRepository
    {
        private readonly OraDatabase db;
        public VoucherTypeRepository(OraDatabase db)
        {
            this.db = db;
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }

        public ACC_VOUCHER_TYPEModel GetById(int id)
        {
            string sp = "pkg_voucher.acc_voucher_type_select";
            try
            {
                var ob = new ACC_VOUCHER_TYPEModel();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pVOUCHERTYPEID", Value =id},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.VOUCHERTYPEID = (dr["VOUCHERTYPEID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VOUCHERTYPEID"]);
                    ob.TYPENAME = (dr["TYPENAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TYPENAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                }
         
           
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ACC_VOUCHER_TYPEModel> GetAll()
        {
            string sp = "pkg_voucher.acc_voucher_type_select";
            try
            {
                var obList = new List<ACC_VOUCHER_TYPEModel>();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_VOUCHER_TYPEModel ob = new ACC_VOUCHER_TYPEModel();
                    ob.VOUCHERTYPEID = (dr["VOUCHERTYPEID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VOUCHERTYPEID"]);
                    ob.TYPENAME = (dr["TYPENAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TYPENAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Save(ACC_VOUCHER_TYPEModel model)
        {
            const string sp = "pkg_voucher.acc_voucher_type_save";
            string jsonStr = "{";
            var ob = model;
            var i = 1;
            try
            {

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pVOUCHERTYPEID", Value = ob.VOUCHERTYPEID},
                     new CommandParameter() {ParameterName = "pTYPENAME", Value = ob.TYPENAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SelectModel> GetVoucherTypeSelectModels(string comp_code)
        {
            string sp = "pkg_voucher.acc_voucher_type_select";
            try
            {
                var obList = new List<SelectModel>();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SelectModel ob = new SelectModel();
                    ob.Value = (dr["VOUCHERTYPEID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VOUCHERTYPEID"]);
                    ob.Text = (dr["TYPENAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TYPENAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteVoucherType(int id)
        {
            string sql = string.Format(@"delete from ACC_VOUCHER_TYPE where VOUCHERTYPEID='{0}'",id);
            return db.ExecNoneQuery(sql) > 0;
        }
    }
}

    

