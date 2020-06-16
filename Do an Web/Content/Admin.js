var app = angular.module("myApp", ["ngRoute"])
    .config(($routeProvider, $locationProvider) => {

        $routeProvider
            .when('/post', {
                templateUrl: '/Admin/Home.html',
                controller: 'AdminPost'
            })
        $locationProvider.html5Mode(false).hashPrefix('!');
    })
    .controller("AdminPost", function ($scope, $http) {
        var base_url = location.protocol + '//' + document.domain + ':' + location.port;
        $http.get(base_url + '/Admin/List').then((result) => {
            $scope.data = result.data;
            console.log(result.data);

        })

    })