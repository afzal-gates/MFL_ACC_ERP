using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ERP.Data;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.BLL
{
    public class UnilayerChartofAccountService : IUnilayerChartofAccountService
    {
       private readonly IParentClassRepository _parentClassRepository;
        private readonly IAccountClassRepository _accountClassRepository;
        private readonly IMainClassRepository _mainClassRepository;
        private readonly ISubClassRepositroy _subClassRepository;
        private readonly IMapClassRepository _mapClassRepository;
        private readonly IVoucherMasterRepository _voucherMasterRepository;
        private ACC_SUB_CLASSModel Treemodel;
        public UnilayerChartofAccountService(ISubClassRepositroy subClassRepository, IMainClassRepository mainClassRepository, IParentClassRepository parentClassRepository, IAccountClassRepository accountClassRepository, IMapClassRepository mapClassRepository, IVoucherMasterRepository voucherMasterRepository)
        {
            this._parentClassRepository = parentClassRepository;
            this._accountClassRepository = accountClassRepository;
            _mapClassRepository = mapClassRepository;
            _voucherMasterRepository = voucherMasterRepository;
            this._mainClassRepository = mainClassRepository;
            this._subClassRepository = subClassRepository;
            this.Treemodel = _mapClassRepository.GetTreeEx();
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
               expanded = true,
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
                expanded =ac.AC_CODE== Treemodel.AC_CODE,
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
                //  expanded = mc.AC_CODE + mc.MAIN_CODE == Treemodel.MAIN_CODE,
                expanded = false,
                items = GetMapClass(mc.AC_CODE, mc.MAIN_CODE).ToList()
            });




        }

        public IEnumerable<TreeView> GetMapClass(string accode, string mainCode)
        {

            List<ACC_MAP_CLASS> mapClass = _mapClassRepository.GetAll(CompanyCode.comp_code, accode, mainCode,"0000");
     
            return mapClass.Select(mp => new TreeView
            {
                recordId = mp.MAP_CLASS_ID,
                controlCode = "N",
                parentCode = mp.AC_CODE + mp.MAIN_CODE,
                code = mp.AC_CODE + mp.MAIN_CODE + mp.MAP_CODE,
                mapCode = mp.MAP_CODE,
                text = mp.MAP_NAME,
                // expanded = mp.MAP_CODE== Treemodel.MAP_CODE,
                expanded = false,
                items = GetSubClass(mp.MAP_CODE).ToList()
            });

        }

        private IEnumerable<TreeView> GetSubClass(string pcode)
        {
            List<ACC_SUB_CLASSModel> subClass = _subClassRepository.GetAllByParentCode(CompanyCode.comp_code, pcode);
            return subClass.Select(sc => new TreeView
            {
                recordId = sc.SUB_CLASS_ID,
                controlCode = "G",
                parentCode = sc.AC_CODE + sc.MAIN_CODE,
                code = sc.AC_CODE + sc.MAIN_CODE + sc.SUB_CODE,
                text =sc.AC_CODE + sc.MAIN_CODE + sc.SUB_CODE+"--"+sc.SUB_NAME,
                expanded = false,
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
                    bool exist = _mainClassRepository.IsMainClassExist(CompanyCode.comp_code, mc.MAIN_NAME, mc.MAIN_CLASS_ID);
                    if (exist)
                    {
                        throw new MultiTexInvalidDataException("Group Name :" + mc.MAIN_NAME + " already exist !");
                    }
                    else
                    {
                        _mainClassRepository.Save(mc);
                        
                    }
                 
                    break;
                case "N":
                    var subCode = _subClassRepository.NextCode(model.code.Substring(0, 4), CompanyCode.comp_code);
                    var sc = new ACC_SUB_CLASSModel()
                    {
                        AC_CODE = model.code.Substring(0,2),
                        MAIN_CODE = model.code.Substring(2,2),
                        SUB_CODE = subCode,
                        MAP_CODE = model.code.Substring(4, 4),
                        COMP_CODE = CompanyCode.comp_code,
                        SUB_NAME = model.text
                    };

                    exist = _subClassRepository.IsSubClassExist(CompanyCode.comp_code, sc.SUB_NAME, sc.SUB_CLASS_ID);
                    if (exist)
                    {
                        throw new MultiTexInvalidDataException("Account Head:" + sc.SUB_NAME + " already exist !");
                    }
                    else
                    {
                        _subClassRepository.Save(sc);
                        
                    }
                 
              
                    model.recordId = sc.SUB_CLASS_ID;
                    break;
                case "M":
                    var mapcode = _mapClassRepository.NextCode(CompanyCode.comp_code);
                    var mapClass = new ACC_MAP_CLASS()
                    {
                        AC_CODE = model.parentCode,
                        MAIN_CODE = model.code.Substring(2, 2),
                        MAP_CODE = mapcode,
                        COMP_CODE = CompanyCode.comp_code,
                        MAP_NAME= model.text,
                        PRNT_CODE = "0000",
                       
                    };
                    exist = _mapClassRepository.IsMapClassExist(CompanyCode.comp_code, mapClass.MAP_NAME, mapClass.MAP_CLASS_ID);
                    if (exist)
                    {
                        throw new MultiTexInvalidDataException("Group Name:" + mapClass.MAP_NAME + " already exist !");
                    }
                    else
                    {
                        _mapClassRepository.Save(mapClass);
                        
                    }
                 
           
                    model.recordId = mapClass.MAP_CLASS_ID;
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
                    bool exist = _mainClassRepository.IsMainClassExist(CompanyCode.comp_code, mc.MAIN_NAME, mc.MAIN_CLASS_ID);
                    if (exist)
                    {
                        throw new MultiTexInvalidDataException("Group Name :" + mc.MAIN_NAME + " already exist !");
                    }
                    else
                    {
                        _mainClassRepository.Save(mc);
                        
                    }
                    break;
                case "N":
                    var mapc = new ACC_MAP_CLASS()
                    {
                        MAP_CLASS_ID = model.recordId,
                        COMP_CODE = CompanyCode.comp_code,
                        MAP_NAME = model.text
                   
                    };
                    exist = _mapClassRepository.IsMapClassExist(CompanyCode.comp_code, mapc.MAP_NAME, mapc.MAP_CLASS_ID);
                    if (exist)
                    {
                        throw new MultiTexInvalidDataException("Group Name:" + mapc.MAP_NAME + " already exist !");
                    }
                    else
                    {
                        _mapClassRepository.Save(mapc);
                        
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
                    exist = _subClassRepository.IsSubClassExist(CompanyCode.comp_code, scEdit.SUB_NAME, scEdit.SUB_CLASS_ID);
                    if (exist)
                    {
                        throw new MultiTexInvalidDataException("Account Head:" + scEdit.SUB_NAME + " already exist !");
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
            List<ACC_SUB_CLASSModel> glHeads = new List<ACC_SUB_CLASSModel>();
            if (!String.IsNullOrEmpty(searchKey))
            {
                 glHeads = _subClassRepository.GetAccountHeards(compCode, searchKey);
            }
            return glHeads;
        }
        public TreeView GetMainClassHeads(string compCode)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TreeView> GetControlChartOfAccounts()
        {
            var parentclasList = _parentClassRepository.GetAll(CompanyCode.comp_code);
            var parents = parentclasList.Select(x => new TreeView
            {
                recordId = x.AC_PARENT_CLASS_ID,
                controlCode = "T",
                parentCode = "0",
                code = x.PARENT_CODE,
                text = x.NAME,
                expanded = true,
            }).ToList();
            foreach (var tree in parents)
            {
                tree.items = GetControlAccountClass(tree.code).ToList();
                yield return tree;
            }
        }

        public List<SelectModel> GetCashBankHeads(string compCode, string mapCode)
        {
            return _mainClassRepository.GetCashBankHeads(compCode, mapCode);
        }

        public bool CheckGlTransactionExist(string compCode, string glCode, string control_code)
        {
            if (control_code.Equals("G"))
            {
                return _voucherMasterRepository.CheckGlTransactionExist(compCode, glCode);
            }
            else
            {
                throw new MultiTexArgumentMissingException("Invalid Requiest !!");
            }
          
            
        }

        public bool DeleteGlAccount(string compCode, string glCode, string controlCode, int id)
        {
            if (CheckGlTransactionExist(compCode, glCode, controlCode))
            {
                throw new MultiTexArgumentMissingException("Transaction exist of this account !");
            }
            else
            {
               int deleted= _subClassRepository.Delete(compCode, glCode, id);
                if (deleted==0)
                {
                    throw new MultiTexArgumentMissingException("GL account not delete !");
                }
               return deleted > 0;
            }

          
        }


        private IEnumerable<TreeView> GetControlAccountClass(string parentCode)
        {
            List<ACC_AC_CLASSModel> accClass = _accountClassRepository.GetAll(CompanyCode.comp_code, parentCode);
            return accClass.Select(ac => new TreeView
            {
                recordId = ac.AC_CLASS_ID,
                controlCode = "A",
                parentCode = parentCode,
                code = ac.AC_CODE,
                text = ac.CLASS_NAME,
                expanded = true,
                items = GetControlMainClass(ac.AC_CODE).ToList()
            });
        }
        private IEnumerable<TreeView> GetControlMainClass(string parentCode)
        {
            List<ACC_MAIN_CLASSModel> mainClass = _mainClassRepository.GetAll(CompanyCode.comp_code, parentCode);
            return mainClass.Select(mc => new TreeView
            {
                recordId = mc.MAIN_CLASS_ID,
                controlCode = "M",
                parentCode = mc.AC_CODE,
                code = mc.AC_CODE + mc.MAIN_CODE,
                text = mc.MAIN_NAME,
                expanded = true,
                items = GetControlMapClass(mc.AC_CODE, mc.MAIN_CODE).ToList()
            });
        }

        public IEnumerable<TreeView> GetControlMapClass(string accode, string mainCode)
        {

            List<ACC_MAP_CLASS> mapClass = _mapClassRepository.GetAll(CompanyCode.comp_code, accode, mainCode, "0000");

            return mapClass.Select(mp => new TreeView
            {
                recordId = mp.MAP_CLASS_ID,
                controlCode = "N",
                parentCode = mp.AC_CODE + mp.MAIN_CODE,
                code = mp.AC_CODE + mp.MAIN_CODE + mp.MAP_CODE,
                mapCode = mp.MAP_CODE,
                text = mp.MAP_NAME,
                expanded = false,
            
            });

        }
    

    }


}