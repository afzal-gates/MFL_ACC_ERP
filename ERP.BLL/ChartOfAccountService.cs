using System.Collections.Generic;
using System.Data;
using System.Linq;
using ERP.Data;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.BLL
{
    public class ChartOfAccountService : IChartOfAccountService
    {
        private readonly IParentClassRepository _parentClassRepository;
        private readonly IAccountClassRepository _accountClassRepository;
        private readonly IMainClassRepository _mainClassRepository;
        private readonly ISubClassRepositroy _subClassRepository;
        public ChartOfAccountService(ISubClassRepositroy subClassRepository,IMainClassRepository mainClassRepository,IParentClassRepository parentClassRepository, IAccountClassRepository accountClassRepository)
        {
            this._parentClassRepository = parentClassRepository;
            this._accountClassRepository = accountClassRepository;
            this._mainClassRepository = mainClassRepository;
            this._subClassRepository = subClassRepository;
        }
        public IEnumerable<TreeView> GetChartOfAccount()
        {
            var parentclasList = _parentClassRepository.GetAll(CompanyCode.comp_code);
            var parents = parentclasList.Select(x => new TreeView
            {
               recordId =x.AC_PARENT_CLASS_ID,
               controlCode ="T",
                parentCode = "0",
                code = x.PARENT_CODE,
                text = x.NAME,
            }).ToList();
            foreach (var tree in parents)
            {
                tree.items = GetAccountClass(tree.code).ToList();
                yield return tree;
            }
        }
        #region
        private IEnumerable<TreeView> GetAccountClass(string parentCode)
        {
            List<ACC_AC_CLASSModel> accClass = _accountClassRepository.GetAll(CompanyCode.comp_code,parentCode);
            return accClass.Select(ac => new TreeView
            {
                recordId = ac.AC_CLASS_ID,
                controlCode = "A",
                parentCode = parentCode,
                code = ac.AC_CODE,
                text = ac.CLASS_NAME,
                items = GetMainClass(ac.AC_CODE).ToList()
            });
        }

        private IEnumerable<TreeView> GetMainClass(string parentCode)
        {
            List<ACC_MAIN_CLASSModel> mainClass = _mainClassRepository.GetAll(CompanyCode.comp_code, parentCode);
            return mainClass.Select(mc => new TreeView
            {
                recordId = mc.MAIN_CLASS_ID,
                controlCode = "M",
                parentCode = mc.AC_CODE,
                code = mc.AC_CODE + mc.MAIN_CODE,
                text = mc.MAIN_NAME,
                items = GetSubClass(mc.AC_CODE, mc.MAIN_CODE).ToList()
            });
        }

        private IEnumerable<TreeView> GetSubClass(string accode,string mainCode)
        {
            List<ACC_SUB_CLASSModel> subClass = _subClassRepository.GetAll(CompanyCode.comp_code, accode,mainCode);
            return subClass.Select(sc => new TreeView
            {
                recordId = sc.SUB_CLASS_ID,
                controlCode = "G",
                parentCode = sc.AC_CODE + sc.MAIN_CODE,
                code = sc.AC_CODE + sc.MAIN_CODE + sc.SUB_CODE,
                text = sc.SUB_NAME
            });
        }

        #endregion

        public TreeView SaveChartOfAccount(TreeView model)
        {
            switch (model.controlCode)
            {
                case "T":
                    break;
                case "A":
                    var mainCode= _mainClassRepository.NextCode(model.code, CompanyCode.comp_code);
                    var mc = new ACC_MAIN_CLASSModel()
                    {
                        AC_CODE = model.code,
                        MAIN_CODE = mainCode,
                        COMP_CODE = CompanyCode.comp_code,
                        MAIN_NAME = model.text
                    };
                    bool exist = _mainClassRepository.IsMainClassExist(CompanyCode.comp_code, model.text, mc.MAIN_CLASS_ID);
                    if (exist)
                    {
                        throw new MultiTexInvalidDataException("Group Name :" + model.text + " already exist !");
                    }
                    else
                    {
                        _mainClassRepository.Save(mc);
                        
                    }
               
                    break;
                case "M":
                    var subCode = _subClassRepository.NextCode( model.code, CompanyCode.comp_code);
                    var sc = new ACC_SUB_CLASSModel()
                    {
                        AC_CODE = model.parentCode,
                        MAIN_CODE = model.code.Substring(2,2),
                        SUB_CODE = subCode,
                        COMP_CODE = CompanyCode.comp_code,
                        SUB_NAME = model.text
                    };
                     exist = _subClassRepository.IsSubClassExist(CompanyCode.comp_code, model.text,sc.SUB_CLASS_ID);
                     if (exist)
                     {
                         throw new MultiTexInvalidDataException("Account Head :" + model.text + " already exist !");
                     }
                     else
                     {
                         _subClassRepository.Save(sc);

                     }
                    model.recordId = sc.SUB_CLASS_ID;
                    break;
                case "G":
                    break;

            }
            return model;
        }

        public TreeView UpdateChartOfAccount(string code, string controlCode, TreeView model)
        {

            switch (controlCode)
            {
                case "T":
                    break;
                case "A":
                    break;
                case "M":
                    var mc = new ACC_MAIN_CLASSModel()
                    {
                        MAIN_CLASS_ID = model.recordId,
                        AC_CODE = model.parentCode,
                        MAIN_CODE = model.code,
                        COMP_CODE = CompanyCode.comp_code,
                        MAIN_NAME = model.text
                    };
                    bool exist=  _mainClassRepository.IsMainClassExist(CompanyCode.comp_code, model.text,mc.MAIN_CLASS_ID);
                    if (exist)
                    {
                        throw new MultiTexInvalidDataException("Group Name :" + mc.MAIN_NAME + " already exist !");
                    }
                    else
                    {
                        _mainClassRepository.Save(mc);
                        
                    }
                    break;
                case "G":
                    var scEdit = new ACC_SUB_CLASSModel()
                    {
                        SUB_CLASS_ID = model.recordId,
                        AC_CODE = model.parentCode,
                        MAIN_CODE = model.code,
                        SUB_CODE = model.code,
                        SUB_NAME = model.text,
                        COMP_CODE = CompanyCode.comp_code
                    };

                     exist = _subClassRepository.IsSubClassExist(CompanyCode.comp_code, model.text,scEdit.SUB_CLASS_ID);
                     if (exist)
                     {
                         throw new MultiTexInvalidDataException("Account Head :" + model.text + " already exist !");
                     }
                     else
                     {
                         _subClassRepository.Save(scEdit);

                     }
                    break;
            }
            return model;
        }

        public List<ACC_SUB_CLASSModel> GetAccountHeards(string compCode, string searchKey)
        {
            List<ACC_SUB_CLASSModel> dataTable =  _subClassRepository.GetAccountHeards( compCode,  searchKey);
            return dataTable;
        }
        public TreeView GetMainClassHeads(string compCode)
        {
            throw new System.NotImplementedException();
        }
    }


}
