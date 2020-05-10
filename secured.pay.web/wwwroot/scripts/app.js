var app = angular.module("pay-module", ['pay-service']);

app.controller("payCTRL", function ($scope, payService) {

    $scope.initialize = function () {
        $scope.isTransComplete = false;
        const params = new URLSearchParams(window.location.search);
        if (params.has('id')) {
            var id = params.get('id');
            $scope.TransactionStepID = id;
            if (id !== undefined && id !== null && id !== "") {
                orderRequest = {};
                orderRequest.TransactionStepID = id;
                payService.getOrder(orderRequest)
                    .then(function (response) {
                        var result = response.data;
                        $scope.key = result.Key;
                        $scope.success_url = result.Response.SucessUrl;
                        $scope.amount = result.Response.RazorPay_Attribute.amount;
                        $scope.order_id = result.Response.RazorPay_Attribute.id;
                        $scope.name = result.Response.PayeeName;
                        $scope.description = ""; //
                        $scope.img = ""; //
                        $scope.payee_name = result.Response.PayeeName;
                        $scope.payee_email = result.Response.Email;
                        $scope.payee_mobno = result.Response.MobileNo;

                        pay();

                    }, function (err) {
                        coreService.hideInd();
                        console.log(err.data);
                    });
            }
        }
    };

    var pay = function () {
       
        var options = {
            "key": $scope.key,
            "amount": $scope.amount, 
            "currency": "INR",
            "name": $scope.name,
            "description": "Test Transaction", ////
            "image": "", ///
            "order_id": $scope.order_id, 
            "handler": function (response) {
                console.log(response);
                $scope.orderRequest = {};
                $scope.orderRequest.OrderID = response.razorpay_order_id;
                $scope.orderRequest.Signature = response.razorpay_signature;
                $scope.orderRequest.PaymentID = response.razorpay_payment_id;
                $scope.orderRequest.TransactionStepID = $scope.TransactionStepID;
                success($scope.orderRequest);
            },
            "prefill": {
                "name": $scope.payee_name,
                "email": $scope.payee_email,
                "contact": $scope.payee_mobno
            },
            "notes": {
                "address": "Razorpay Corporate Office"
            },
            "theme": {
                "color": "#F37254"
            }
        };
        var rzp1 = new Razorpay(options);
        document.getElementById('rzp-button1').onclick = function (e) {
            rzp1.open();
            e.preventDefault();
        };
    };

    var success = function (orderRequest) {
        $scope.isTransComplete = true;
        payService.pay(orderRequest)
            .then(function (response) {
                var result = response.data;
                console.log(result);
                if (result.Status === 'SUCCESS') {
                    window.location.href = $scope.success_url;
                }
                else {
                    console.log(result);
                }
            }, function (err) {
                console.log(err.data);
            });
    };
});

