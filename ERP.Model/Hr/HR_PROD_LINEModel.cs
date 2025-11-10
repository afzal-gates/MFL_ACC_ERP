using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

using ERP.DAL;

namespace ERP.Model
{
    public class HR_PROD_LINEModel
    {
        public Int64 HR_PROD_LINE_ID { get; set; }
        public Int64 HR_PROD_FLR_ID { get; set; }
        public string LINE_NO { get; set; }
        public string LINE_CODE { get; set; }
        public string LINE_DESC_EN { get; set; }
        public string LINE_DESC_BN { get; set; }
        public Int64 LK_GARM_TYPE_ID { get; set; }
        public Int64 LK_FLOOR_ID { get; set; }
        public string IS_ACTIVE { get; set; }

        public Int64? GMT_PLN_RSRC_ACCESS_ID { get; set; }


        public Int64 TTL_REQ_OP { get; set; }
        public Int64 TTL_REQ_HP { get; set; }
        public Int64 TTL_PRE_OP { get; set; }
        public Int64 TTL_PRE_HP { get; set; }
        public Int64 TTL_H1 { get; set; }
        public Int64 TTL_H2 { get; set; }
        public Int64 TTL_H3 { get; set; }
        public Int64 TTL_H4 { get; set; }
        public Int64 TTL_H5 { get; set; }
        public Int64 TTL_H6 { get; set; }
        public Int64 TTL_H7 { get; set; }
        public Int64 TTL_H8 { get; set; }
        public Int64 TTL_OT_TARGET { get; set; }
        public Int64 TTL_OT_PROD { get; set; }
        public Int64 TTL_OT_HR { get; set; }
        public long LN_TTL_TARGET { get; set; }

        public long LN_TTL_PROD { get; set; }

        private List<string> _MergeLine = null;

        public List<string> MergeLine
        {
            get
            {
                if (_MergeLine == null)
                {
                    _MergeLine = new List<string>();
                }
                return _MergeLine;
            }
            set
            {
                _MergeLine = value;
            }
        }


   



        public string Save()
        {
            const string sp = "pkg_hr.hr_prod_line_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                     new CommandParameter() {ParameterName = "pLINE_CODE", Value = ob.LINE_CODE},
                     new CommandParameter() {ParameterName = "pLINE_DESC_EN", Value = ob.LINE_DESC_EN},
                     new CommandParameter() {ParameterName = "pLINE_DESC_BN", Value = ob.LINE_DESC_BN},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = 0 /* TODO MIGRATION: Pass userId as parameter - HttpContext.Current.Session["multiScUserId"] */},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = 0 /* TODO MIGRATION: Pass userId as parameter - HttpContext.Current.Session["multiScUserId"] */},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opHR_PROD_LINE_ID", Value =0, Direction = ParameterDirection.Output},
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

        public List<HR_PROD_LINEModel> SelectAll()
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.LINE_DESC_BN = (dr["LINE_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_BN"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);

                    ob.LK_FLOOR_ID_SPAN = (dr["HR_PROD_FLR_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SPAN"]);
                    ob.LK_FLOOR_ID_SL = (dr["HR_PROD_FLR_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SL"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HR_PROD_LINEModel> SelectByID(Int64? pHR_PROD_LINE_ID = null, Int64? pHR_PROD_FLR_ID = null, Int64? pHR_PROD_BLDNG_ID = null, Int64? pHR_OFFICE_ID = null)
        {
            string sp = "pkg_hr.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =pHR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_BLDNG_ID", Value =pHR_PROD_BLDNG_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value =pHR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.LINE_DESC_BN = (dr["LINE_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_BN"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_PROD_BLDNG_ID = (dr["HR_PROD_BLDNG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_BLDNG_ID"]);
                    
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);

                    ob.BLDNG_CODE = (dr["BLDNG_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_CODE"]);
                    ob.BLDNG_DESC_EN = (dr["BLDNG_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_DESC_EN"]);
                    ob.BLDNG_NO = (dr["BLDNG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_NO"]);

                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.COMP_DESC = (dr["COMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_DESC"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);

                    ob.OFFICE_CODE = (dr["OFFICE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_CODE"]);
                    ob.OFFICE_DESC = (dr["OFFICE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_DESC"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        public List<HR_PROD_LINEModel> getFloorData()
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public HR_PROD_LINEModel Select(long ID)
        {
            string sp = "Select_HR_PROD_LINE";
            try
            {
                var ob = new HR_PROD_LINEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.LINE_DESC_BN = (dr["LINE_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_BN"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long LK_FLOOR_ID_SL { get; set; }

        public long LK_FLOOR_ID_SPAN { get; set; }

        public string FLOOR_NO { get; set; }

        public string FLOOR_CODE { get; set; }

        public string FLOOR_DESC_EN { get; set; }

        public long TTL_MC { get; set; }

        public long TTL_TARGET { get; set; }

        public Int64 G_TARGET { get; set; }
        public Int64 G_PROD { get; set; }


        public long TTL_H1_F { get; set; }

        public long TTL_H2_F { get; set; }

        public long TTL_H3_F { get; set; }

        public long TTL_H4_F { get; set; }

        public long TTL_H5_F { get; set; }

        public long TTL_H6_F { get; set; }

        public long TTL_H7_F { get; set; }

        public long TTL_H8_F { get; set; }

        public long TTL_TARGET_F { get; set; }

        public long TTL_OT_TARGET_F { get; set; }

        public long TTL_OT_PROD_F { get; set; }

        public long TTL_OT_HR_F { get; set; }

        public long G_PROD_F { get; set; }

        public long G_TARGET_F { get; set; }

        public int TTL_USE_OP { get; set; }

        public int TTL_USE_HP { get; set; }

        public decimal G_TTL_VALUE { get; set; }

        public long G_TARGET_CUR { get; set; }

        public decimal G_LN_ACHV { get; set; }


        public decimal LN_EFF { get; set; }

        public decimal TTL_SMV { get; set; }

        public decimal INPUT_MAN_MIN { get; set; }

        public decimal OUTPUT_MAN_MIN { get; set; }

        public Int64 TTL_CT_PRD_QTY { get; set; }

        public Int64 TTL_CT_TGT_QTY { get; set; }

        public decimal TTL_CT_ACHV { get; set; }

        public string BLDNG_CODE { get; set; }

        public string BLDNG_DESC_EN { get; set; }

        public string BLDNG_NO { get; set; }

        public string COMP_CODE { get; set; }

        public string COMP_DESC { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string COMP_SNAME { get; set; }

        public string OFFICE_CODE { get; set; }

        public string OFFICE_DESC { get; set; }

        public string OFFICE_NAME_EN { get; set; }

        public long HR_OFFICE_ID { get; set; }

        public long HR_PROD_BLDNG_ID { get; set; }
        public long? COUNT_GMT_CAT { get; set; }
        public long HR_COMPANY_ID { get; set; }

        public List<HR_PROD_LINEModel> getLineByProdType(Int64 pGMT_PRODUCT_TYP_ID, Int64 pHR_PROD_FLR_ID)
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = pGMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();

                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.COUNT_GMT_CAT = (dr["COUNT_GMT_CAT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNT_GMT_CAT"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<HR_PROD_LINEModel> GetPlnRsurcAccessLnByFlrUsr(Int64 pSC_USER_ID, Int64 pHR_PROD_FLR_ID)
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();

                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);

                    ob.GMT_PLN_RSRC_ACCESS_ID = (dr["GMT_PLN_RSRC_ACCESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_RSRC_ACCESS_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACTIVE"]);
                    
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