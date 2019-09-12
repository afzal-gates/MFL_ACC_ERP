using ERP.Data;
using ERP.Shared;
using System;
namespace ERP.BLL
{
   public class CheckerMakerService: ICheckerMakerService
    {
       private readonly ICheckerMakerRepository checkerMakerRepository;
        private readonly IVoucherMasterRepository _voucherMasterRepository;
        public CheckerMakerService(ICheckerMakerRepository checkerMakerRepository, IVoucherMasterRepository voucherMasterRepository)
        {
            this.checkerMakerRepository = checkerMakerRepository;
            this._voucherMasterRepository = voucherMasterRepository;
        }

        public int UpdateBillReconciliation()
        {
            return _voucherMasterRepository.UpdateBillKey(CompanyCode.comp_code);
        }

        public int UpdateCheckerMaker()
        {
            return checkerMakerRepository.UpdateCheckerMaker(CompanyCode.comp_code);
        }
    }
}
