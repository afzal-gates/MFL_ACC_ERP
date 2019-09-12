
(function () {
    "use strict";
    angular.module("multitex.accounting").constant("dataConstants", {
      
        VOUCHER_TYPE_URL: '/' + 'api/accounting/voucher-types/',
        CHART_OF_ACCOUNT_URL: '/' + 'api/accounting/chart-of-accounts/',
        UNILAYER_CHART_OF_ACCOUNT_URL: '/' + 'api/accounting/unilayer-chart-of-accounts/',
        COST_CENTER: '/' + 'api/accounting/cost-centers/',
        CURRENCY: '/' + 'api/accounting/currencies/',
        VOUCHER_MASTER: '/' + 'api/accounting/voucher-masters/',
        COMPANY_URL: '/' + 'api/accounting/companies/',

        PAYMENT_MODE_URL: '/' + 'api/accounting/payment-modes/',
        DRAFT_VOUCHER: '/' + 'api/accounting/draft-vouchers/',
        CHECKER_MAKER_URL: '/' + 'api/accounting/checker-makers/',
        COST_CENTER_GROUP: '/' + 'api/accounting/cost-center-groups/',
        STOCK_CLOSING: '/' + 'api/accounting/stock-closings/',
        
    });
})();