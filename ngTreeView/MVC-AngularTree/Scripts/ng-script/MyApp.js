(function () {

    //angular module
    var myApp = angular.module('myApp', ['angularTreeview']);

    //controller
    myApp.controller('myController', function ($scope, $http) {
        fetch();
        function fetch() {
            $http({
                method: 'GET',
                url: '/api/Roles'
            }).then(function successCallback(response) {
                console.log(response.data.objRole);
                $scope.RoleList = response.data.objRole;               
            }, function errorCallback(response) {
                console.log(response);
            });
        }

        this.OnSelectNode = function (node) {

        }

        this.AddNode = function () {
            $scope.RoleList[0].Children.push({
                RoleID: 13,
                RoleName: "Test",
                ParentID: 1,
                Child: {},
                Collapsed: true,
                IsDirectory: true,
                Icon: "folder",
                Level:1,
                Children: []
            });
        }
    });

})();

