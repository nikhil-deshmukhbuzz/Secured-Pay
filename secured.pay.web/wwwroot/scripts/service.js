var serv = angular.module('pay-service', []);

serv.constant('config', {
   // payUrl: 'http://localhost:58803/api'
    payUrl: 'https://b131stolj4.execute-api.ap-south-1.amazonaws.com/Prod/api'

});

serv.service('payService', function ($http, config) {

    this.getOrder = function (orderRequest) {
        return $http.post(config.payUrl + '/Payment/GetOrder', orderRequest);

    };

    this.pay = function (orderRequest) {
        return $http.post(config.payUrl + '/Payment/Pay', orderRequest);

    };
});