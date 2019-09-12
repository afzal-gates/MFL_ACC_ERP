(function () {
    'use strict';
    angular
        .module('multitex.accounting')
        .run(appRun);
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [

            {
                state: 'VchTypeList',
                config: {
                    url: '/vchTypeList',
                    views: {
                        "VchTypeList": {
                            controller: 'voucherTypeListController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/Accnt/_VchTypeList'
                        }
                    },

                    Title: 'Voucher Type',
                    reloadOnSearch: false
                }
            },

            {
                state: 'VchType',
                config: {
                    url: '/vchType/:voucherTypeId',
                    views: {
                        "VchType": {
                            controller: 'VchTypeController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/Accnt/_VchType'
                        }
                    },
                    Title: 'Voucher Type',
                    reloadOnSearch: false
                }
            },

            {
                state: 'Teeview',
                config: {
                    url: '/treeView',
                    views: {
                        "Teeview": {
                            controller: 'chartOfAccountController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/ChartOfAccount/_TreeView'
                        }
                    },

                    Title: 'Chart Of Account',
                    reloadOnSearch: false
                }
            },
            {
                state: 'unilayerchartOfaccounts',
                config: {
                    url: '/unilayerchartOfaccounts',
                    views: {
                        "unilayerchartOfaccounts": {
                            controller: 'unilayerChartOfAccountController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/UnilayerChartOfAccount/_TreeView'
                        }
                    },

                    Title: 'Unilayer Chart Of Account',
                    reloadOnSearch: false
                }
            },
            {
                state: 'cost-centers',
                config: {
                    url: '/costCenters',
                    views: {
                        "cost-centers": {
                            controller: 'costCenterController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/CostCenter/_CostCenters'
                        }
                    },

                    Title: 'Cost Center',
                    reloadOnSearch: false
                }
            },
            {
                state: 'cost-center-add',
                config: {
                    url: '/costCenter/:id',
                    views: {
                        "cost-center-add": {
                            controller: 'costCenterAddController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/CostCenter/_Edit'
                        }
                    },

                    Title: 'Cost Center ',
                    reloadOnSearch: false
                }
            },
            {
                state: 'cost-center-groups',
                config: {
                    url: '/costCenterGroups',
                    views: {
                        "cost-center-groups": {
                            controller: 'costCenterGroupsController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/CostCenterGroup/_CostCenterGroups'
                        }
                    },

                    Title: 'Cost Center Group',
                    reloadOnSearch: false
                }
            },
            {
                state: 'cost-center-group-add',
                config: {
                    url: '/costCenterGroup/:id',
                    views: {
                        "cost-center-group-add": {
                            controller: 'costCenterGroupAddController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/CostCenterGroup/_Edit'
                        }
                    },

                    Title: 'Cost Center Group ',
                    reloadOnSearch: false
                }
            },
            //voucher master
            {
                state: 'voucher-masters',
                config: {
                    url: '/voucherMasters',
                    views: {
                        "voucher-masters": {
                            controller: 'voucherMastersController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/VoucherMaster/_VoucherList'
                        }
                    },

                    Title: 'Voucher Posting',
                    reloadOnSearch: false
                }
            },
                //Draft voucher
            {
                state: 'draft-vouchers',
                config: {
                    url: '/draftVouchers',
                    views: {
                        "draft-vouchers": {
                            controller: 'draftVouchersController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/DraftVoucher/_VoucherList'
                        }
                    },

                    Title: 'Draft Voucher Posting',
                    reloadOnSearch: false
                }
            },


            {
                state: 'companies',
                config: {
                    url: '/companies',
                    views: {
                        "companies": {
                            controller: 'companiesController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/Company/_Companies'
                        }
                    },

                    Title: 'Company',
                    reloadOnSearch: false
                }
            },
            {
                state: 'company-add',
                config: {
                    url: '/company/:id',
                    views: {
                        "company-add": {
                            controller: 'companyAddController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/Company/_Edit'
                        }
                    },

                    Title: 'Company',
                    reloadOnSearch: false
                }
            },
            {
                state: 'paymentmodes',
                config: {
                    url: '/paymentmodes',
                    views: {
                        "paymentmodes": {
                            controller: 'paymentModesController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/PaymentMode/_PaymentModes'
                        }
                    },

                    Title: 'PaymentMode',
                    reloadOnSearch: false
                }
            },
            {
                state: 'paymentmode-add',
                config: {
                    url: '/paymentmode/:id',
                    views: {
                        "paymentmode-add": {
                            controller: 'paymentModeAddController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/PaymentMode/_Edit'
                        }
                    },

                    Title: 'payment Mode',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ledgerReport',
                config: {
                    url: '/ledgerReport',
                    views: {
                        "ledgerReport": {
                            controller: 'ledgerReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_Leger'
                        }
                    },

                    Title: 'Ledger',
                    reloadOnSearch: false
                }
            },

             {
                 state: 'legerCostCenter',
                 config: {
                     url: '/legerCostCenter',
                     views: {
                         "ledgerReport": {
                             controller: 'ledgerReportController',
                             controllerAs: 'vm',
                             templateUrl: '/Accounting/AccountingReport/_LegerCostCenter'
                         }
                     },

                     Title: 'Ledger Cost Center',
                     reloadOnSearch: false
                 }
             },

                    {
                        state: 'costCenterLeger',
                        config: {
                            url: '/costCenterLeger',
                            views: {
                                "ledgerReport": {
                                    controller: 'CostCenterLedgerReportController',
                                    controllerAs: 'vm',
                                    templateUrl: '/Accounting/AccountingReport/_CostCenterLeger'
                                }
                            },

                            Title: 'Cost Center Wise Ledger',
                            reloadOnSearch: false
                        }
                    },
            {
                state: 'controlLedgerReport',
                config: {
                    url: '/controlLedgerReport',
                    views: {
                        "controlLedgerReport": {
                            controller: 'controlLedgerReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_ControlLedger'
                        }
                    },

                    Title: 'Control Ledger',
                    reloadOnSearch: false
                }
            },
            {
                state: 'voucherSummaryReport',
                config: {
                    url: '/voucherSummaryReport',
                    views: {
                        "voucherSummaryReport": {
                            controller: 'voucherSummaryReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_VoucherSummary'
                        }
                    },

                    Title: 'voucher Summary',
                    reloadOnSearch: false
                }
            },
            {
                state: 'voucherStatementReport',
                config: {
                    url: '/voucherStatementReport',
                    views: {
                        "voucherStatementReport": {
                            controller: 'voucherSummaryReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_VoucherStatement'
                        }
                    },

                    Title: 'voucher Statement',
                    reloadOnSearch: false
                }
            },

            {
                state: 'balanceSheetReport',
                config: {
                    url: '/balanceSheetReport',
                    views: {
                        "balanceSheetReport": {
                            controller: 'trialBalanceReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_BalanceSheet'
                        }
                    },

                    Title: 'Balance Sheet',
                    reloadOnSearch: false
                }
            },

            {
                state: 'incomeStatementReport',
                config: {
                    url: '/incomeStatementReport',
                    views: {
                        "incomeStatementReport": {
                            controller: 'trialBalanceReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_IncomeStatement'
                        }
                    },

                    Title: 'Balance Sheet',
                    reloadOnSearch: false
                }
            },
            {
                state: 'receivePaymentReport',
                config: {
                    url: '/receivePaymentReport',
                    views: {
                        "receivePaymentReport": {
                            controller: 'receivePaymentReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_ReceivePayment'
                        }
                    },

                    Title: 'Receive Payment',
                    reloadOnSearch: false
                }
            },
            {
                state: 'trialBalanceReport',
                config: {
                    url: '/trialBalanceReport',
                    views: {
                        "trialBalanceReport": {
                            controller: 'trialBalanceReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_TrialBalance'
                        }
                    },

                    Title: 'Trial Balance',
                    reloadOnSearch: false
                }
            },
            {
                state: 'cashBookReport',
                config: {
                    url: '/cashBookReport',
                    views: {
                        "cashBookReport": {
                            controller: 'cashBookReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_CashBook'
                        }
                    },

                    Title: 'Cash Book',
                    reloadOnSearch: false
                }
            },
               {
                   state: 'dayBookReport',
                   config: {
                       url: '/dayBookReport',
                       views: {
                           "dayBookReport": {
                               controller: 'dayBookReportController',
                               controllerAs: 'vm',
                               templateUrl: '/Accounting/AccountingReport/_DayBook'
                           }
                       },

                       Title: 'Day Book',
                       reloadOnSearch: false
                   }
               },
            {
                state: 'bankBookReport',
                config: {
                    url: '/bankBookReport',
                    views: {
                        "bankBookReport": {
                            controller: 'bankBookReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/AccountingReport/_BankBook'
                        }
                    },

                    Title: 'Cash Book',
                    reloadOnSearch: false
                }
            },
            {
                state: 'currencies',
                config: {
                    url: '/currencies',
                    views: {
                        "currencies": {
                            controller: 'currenciesController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/Currency/_Currencies'
                        }
                    },

                    Title: 'Currency',
                    reloadOnSearch: false
                }
            },
            {
                state: 'currency-add',
                config: {
                    url: '/currency/:id',
                    views: {
                        "currency-add": {
                            controller: 'currencyAddController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/Currency/_Edit'
                        }
                    },

                    Title: 'Currency',
                    reloadOnSearch: false
                }
            },
            {
                state: 'checker-makers',
                config: {
                    url: '/checkerMakers',
                    views: {
                        "checker-makers": {
                            controller: 'checkerMakersController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/CheckerMaker/_Edit'
                        }
                    },

                    Title: 'Checker',
                    reloadOnSearch: false
                }
            },
            {
                state: 'stcok-closings',
                config: {
                    url: '/stockclosings',
                    views: {
                        "stcok-closings": {
                            controller: 'stockClosingController',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/StockClosing/_StockClosing'
                        }
                    },

                    Title: 'Stock Closing',
                    reloadOnSearch: false
                }
            },
            {
                state: 'bank-reconcilation-vouchers',
                config: {
                    url: '/bankreconcilationvouchers',
                    views: {
                        "bank-reconcilation-vouchers": {
                            controller: 'bankReconcileVouchers',
                            controllerAs: 'vm',
                            templateUrl: '/Accounting/BankReconcilation/_ReconcileVouchers'
                        }
                    },

                    Title: 'Bank Reconcilation Vouchers',
                    reloadOnSearch: false
                }
            },
        ];
    }
})();
