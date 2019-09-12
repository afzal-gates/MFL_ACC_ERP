using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Data;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.BLL
{
    public class PaymentModeService : IPaymentModeService
    {
        private readonly IPaymentModeRepository paymentModeRepository;
        public PaymentModeService(IPaymentModeRepository paymentModeRepository)
        {
            this.paymentModeRepository = paymentModeRepository;
        }

        public bool DeletePaymentMode(int id)
        {
           return paymentModeRepository.DeletePaymentMode(id);

        }

        public List<ACC_PAYMENT_MODE> GetPayementModes(string searchKey)
        {
          List<ACC_PAYMENT_MODE> paymentModes=  paymentModeRepository.GetPayementModes(searchKey);
            return paymentModes;
        }

        public ACC_PAYMENT_MODE GetPaymentModeById(int id)
        {
            ACC_PAYMENT_MODE paymentMode = new ACC_PAYMENT_MODE();
            if(id > 0)
            {
                 paymentMode = paymentModeRepository.GetPaymentModeById(id);
            }
            else
            {
                paymentMode.REF_CODE = paymentModeRepository.GetNewRefCode(CompanyCode.comp_code);
            }
        
            return paymentMode;
        }

        public List<ACC_PAYMENT_MODE> GetPaymentModes(string comp_code)
        {
          return  paymentModeRepository.GetPaymentModes(comp_code);
        }

        public bool SavePaymentMode(int id, ACC_PAYMENT_MODE model)
        {
            if (id <= 0)
            {
                model.REF_CODE = paymentModeRepository.GetNewRefCode(CompanyCode.comp_code);
            }
            bool isSaved=  paymentModeRepository.SavePaymentMode(id, model);
            return isSaved;
        }
    }
}
