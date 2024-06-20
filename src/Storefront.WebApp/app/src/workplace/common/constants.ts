const constants = {
  versionId: 'hotline-app-version',
  isNeedUpdate: 'needUpdate',
  api: {
    account_host: 'accountApiHost',
    agreement_host: 'agreementApiHost',
    card_host: 'cardApiHost',
    counterpart_host: 'counterpartApiHost',
    ui_host: 'userInterfaceApiSetting',
    user_host: 'userInterfaceApiSetting',
    transaction_host: 'transactionApiHost',
    travel_host: 'travelApiHost',
    modules_host: 'moduleApiHost',
    country_host: 'countryApiHost',
    statement_host: 'statementApiHost'
  },
  auth: {
    tokenKey: 'hotline_jwt',
    idleTime: 1800000,
    idleWarningTime: 120000
  },
  paging: {
    pageIndex: 1,
    pageSize: 10,
    maxPages: 10
  },
  navigateQuery: {
    agreement: {
      agreementId: 'agreement.id',
      isInactive: 'agreement.isInactive',
      isDebt: 'agreement.isDebt',
      pageIndex: 'agreement.pageIndex',
      tabName: 'agreement.tab'
    },
    account: {
      accountId: 'account.id',
      pageIndex: 'account.pageIndex'
    },
    card: {
      cardId: 'card.id',
      pageIndex: 'card.pageIndex',
      cardNum: 'card.num',
      tabName: 'card.tab'
    },
    tariff: {
      type: 'tariff.type',
      clientId: 'client.id',
      cardId: 'card.id',
      agreementId: 'agreement.id'
    },
    statement: {
      clientId: 'client.id',
      agreementId: 'agreement.id'
    },
    travel: {
      travelId: 'travel.id',
      pageIndex: 'travel.pageIndex'
    }
  },
  transaction: {
    pageSize: 20
  },
  transactionFinancialBlocking: {
    pageSize: 5,
    defaultDays: 30
  },
  transactionFinancialUnblocking: {
    pageSize: 3
  },
  clientActivity: {
    filter: {
      defaultDays: 7
    }
  },
  formatting: {
    date: 'YYYY-MM-DD',
    dateSlashed: 'YYYY/MM/DD',
    dateVue: 'DD.MM.YYYY',
    dateTimeVue: 'DD.MM.YYYY, HH:mm:ss',
    timeVue: 'HH:mm:ss',
    expiryDate: 'MM/YYYY',
    dateTime: 'DD.MM.YYYY, HH:mm'
  },
  defaultCardShirtUrl: require('../../assets/images/cards/Default.png'),
  token: {
    pageSize: 5
  },
  blockingReasonText: {
    cardCompromise: 'компрометация карты',
    checkWithClient: 'уточнить у клиент'
  }
};

export default constants;
