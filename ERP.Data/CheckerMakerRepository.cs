using ERP.DAL;
using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
   public class CheckerMakerRepository: ICheckerMakerRepository
    {
        private readonly OraDatabase db;
        private readonly IVoucherMasterRepository voucherMasterRepository;
        public CheckerMakerRepository(OraDatabase db, IVoucherMasterRepository voucherMasterRepository)
        {
            this.db = db;
            this.voucherMasterRepository = voucherMasterRepository;
        }
        public int UpdateCheckerMaker(string comp_code)
        {
            string post_id = "";
            int efr = 0;
            long vm_id = 0;
            string sql = String.Format("select POST_ID, count(*) cc From ACC_VOUCHER_MASTER where COMP_CODE='{0}' group by POST_ID having count(*) > 1 order by POST_ID",comp_code);
            var dt = db.ExecWithSqlQuery(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                post_id = Convert.ToString(dt.Rows[i]["POST_ID"]);
                sql = String.Format("select Voucher_Master_id From ACC_VOUCHER_MASTER where post_id='{0}' and COMP_CODE='{1}' order by VOUCHER_MASTER_ID desc", post_id,comp_code);
                var dtd = db.ExecWithSqlQuery(sql);
                vm_id =Convert.ToInt64(dtd.Rows[0]["Voucher_Master_id"]);
                post_id=voucherMasterRepository.GetNewPostID(comp_code);
                sql = String.Format("update ACC_VOUCHER_MASTER set POST_ID='{2}' where COMP_CODE='{0}' AND Voucher_Master_id='{1}'", comp_code, vm_id,post_id);
                efr+= db.ExecNoneQuery(sql);

            }

            return efr;
        }
    }
}
