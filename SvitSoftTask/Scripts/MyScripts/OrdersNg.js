(function() {


    var OrdersApp = angular.module('OrdersApp', []),
        svitSofthub = $.connection.svitSoftHub;

    OrdersApp.controller('OrdersController', function($scope, orderService) {

        getOrders();

        svitSofthub.client.AddOrder = function (order) {

            $scope.orders.push({
                OrderDate: new Date(order.OrderDate),
                ClientName: order.ClientName,
                OrderNumber: order.OrderNumber

            });
            $scope.$apply();
        }

        function getOrders() {

            orderService.GetOrders()
                .success(function(ords) {


                    var parseords = [];

                    for (var i = 0; i < ords.length; i++) {

                        var value = new Date(parseInt(ords[i].OrderDate.replace("/Date(", "").replace(")/", ""), 10));
                        parseords.push({
                            OrderDate: value,
                            ClientName: ords[i].ClientName,
                            OrderNumber: ords[i].OrderNumber

                        });
                    }

                    $scope.orders = parseords;
                })
                .error(function(error) {
                    $scope.status = 'Unable to load order data: ' + error.message;
                });

        }

    });

    OrdersApp.factory('orderService', [
        '$http', function($http) {
            var orderService = {};

            orderService.GetOrders = function() {

                return $http.get('/Orders/GetOrders');

            };

            return orderService;

        }
    ]);


    $.connection.hub.start();
})();