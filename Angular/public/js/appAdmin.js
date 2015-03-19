var app = angular.module('TifAdmin',['ngRoute']);

app.config(['$routeProvider', '$locationProvider',
    function($routeProvider, $locationProvider) {
        $routeProvider
            .when('/News', {
                templateUrl: '../view/AdminNews.tpl.html',
                controller: 'NewsCtrl'
            })
            .when('/Files', {
                templateUrl: '../view/AdminFiles.tpl.html',
               controller: 'FilesCtrl'
            })
            .when('/Links', {
                templateUrl: '../view/AdminLinks.tpl.html',
                controller: 'LinksCtrl'
            })
            .when('/Users', {
                templateUrl: '../view/AdminUsers.tpl.html',
                controller: 'UsersCtrl'
            })
            .when('/Settings', {
                templateUrl: '../view/AdminSettings.tpl.html',
                controller: 'SettingsCtrl'
            })
            .when('/Logout', {

                controller: 'AdminCtrl'
            });

       $locationProvider.html5Mode(false);
}]);


app.controller('AdminCtrl', ['$scope', '$route', '$routeParams', '$location', 'loginSettings','$rootScope',
                'callsConfig', '$http',
    function($scope, $route, $routeParams, $location,loginSettings, rootScope, callsConfig,$http) {
        this.$route = $route;
        this.$location = $location;
        this.$routeParams = $routeParams;

        rootScope.isAuthorized = function () {
            $http({method: 'GET', url: callsConfig.hasAuth})
                .success(function (data, status, headers, config) {
                    rootScope.loginVisible = loginSettings.hideLogin();
                   // next();
                }).error(function (data, status, headers, config) {
                    rootScope.loginVisible = loginSettings.showLogin();
                    $location.path("/");
                });
        };

        $http({method: 'GET', url: callsConfig.getSettings})
            .success(function(data, status, headers, config) {
                rootScope.Settings = data;
            });

        rootScope.loginVisible = loginSettings.showLogin();
}]);

app.controller('NewsCtrl', ['$scope', 'loginSettings', '$rootScope', '$http', 'callsConfig',
    function($scope, loginSettings, rootScope, $http, callsConfig) {

        rootScope.isAuthorized();

        $http({method: 'GET', url: callsConfig.getNews})
            .success(function(data, status, headers, config) {
                $scope.NewsList = data;
                $scope.createVisible = false;
            });

       var fillTable = function() {  $http({method: 'GET', url: callsConfig.getNews})
            .success(function(data, status, headers, config) {
                $scope.NewsList = data;
                $scope.createVisible = false;
            });
        };

        fillTable();
        $scope.createVisible = false;

        this.ShowCreate = function () {
            $scope.createVisible = true;
        };

        this.SelectedItem = function (selectedID) {
            $http({method: 'GET', url: callsConfig.getNewsById + selectedID})
                .success(function(data, status, headers, config) {
                    $scope.EditItem = data;
                    $scope.createVisible = false;

                });
        };

        this.DeleteItem = function (selectedID) {
            $http({method: 'GET', url: callsConfig.deleteNewsById + selectedID})
                .success(function(data, status, headers, config) {
                    $scope.EditItem = null; //clears delete model from form
                    //refresh grid
                    fillTable();

                });
        };

        return $scope.NewsCtrl = this;
    }
]);

app.controller('FilesCtrl', ['$scope', 'loginSettings','$rootScope', '$http', 'callsConfig',
    function($scope, loginSettings, rootScope, $http, callsConfig) {

        rootScope.isAuthorized();

        var fillTable = function () {
            $http({method: 'GET', url: callsConfig.getFiles})
                .success(function(data, status, headers, config) {
                    $scope.FilesList = data;
                    $scope.createVisible = false;
                });
        };

        fillTable();
        $scope.createVisible = false;

        this.ShowCreate = function () {
            $scope.createVisible = true;
        };

        this.SelectedItem = function (selectedID) {
            $http({method: 'GET', url: callsConfig.getFileById + selectedID})
                .success(function(data, status, headers, config) {
                    $scope.EditItem = data;
                    $scope.createVisible = false;
                });
        };

        this.DeleteItem = function (selectedID) {
            $http({method: 'GET', url: callsConfig.deleteFileById + selectedID})
                .success(function (data, status, headers, config) {
                    $scope.EditItem = null; //clears delete model from form
                    //refresh grid
                    fillTable();

                });
        };

        this.Category = _.flatten(rootScope.Settings.category,'title');

        return $scope.FilesCtrl = this;
    }
]);

app.controller('LinksCtrl', ['$scope', 'loginSettings','$rootScope', '$http', 'callsConfig',
    function($scope, loginSettings, rootScope, $http, callsConfig) {

        rootScope.isAuthorized();

        var fillTable = function() {  $http({method: 'GET', url: callsConfig.getLinks})
            .success(function(data, status, headers, config) {
                $scope.LinksList = data;
                $scope.createVisible = false;
            });
        };

        fillTable();
        $scope.createVisible = false;

        this.ShowCreate = function () {
            $scope.createVisible = true;
        };

        this.SelectedItem = function (selectedID) {
            $http({method: 'GET', url: callsConfig.getLinkById + selectedID})
                .success(function(data, status, headers, config) {
                    $scope.EditItem = data;
                    $scope.createVisible = false;

                });
        };

        this.DeleteItem = function (selectedID) {
            $http({method: 'GET', url: callsConfig.deleteLinkById + selectedID})
                .success(function(data, status, headers, config) {
                    $scope.EditItem = data; //clears delete model from form
                    //refresh grid
                    fillTable();
                });
        };

        return $scope.LinksCtrl = this;
    }
]);


app.controller('UsersCtrl', ['$scope', 'loginSettings','$rootScope', '$http', 'callsConfig',
    function($scope,  loginSettings, rootScope, $http, callsConfig) {

        rootScope.isAuthorized();

        var fillTable = function() {  $http({method: 'GET', url: callsConfig.getUsers})
            .success(function(data, status, headers, config) {
                $scope.UsersList = data;
                $scope.createVisible = false;
            });
        };

        fillTable();
        $scope.createVisible = false;

        this.ShowCreate = function () {
            $scope.createVisible = true;
        };

        this.SelectedItem = function (selectedID) {
            $http({method: 'GET', url: callsConfig.getUserById + selectedID})
                .success(function(data, status, headers, config) {
                    $scope.EditItem = data;
                    $scope.createVisible = false;

                });
        };

        this.DeleteItem = function (selectedID) {
            $http({method: 'GET', url: callsConfig.deleteUserById + selectedID})
                .success(function(data, status, headers, config) {
                    $scope.EditItem = data; //clears delete model from form
                    //refresh grid
                    fillTable();
                });
        };

        return $scope.UsersCtrl = this;
    }
]);

app.controller('SettingsCtrl', ['$scope', 'loginSettings','$rootScope', '$http', 'callsConfig',
    function($scope, loginSettings, rootScope, $http, callsConfig) {

        rootScope.isAuthorized();

        var fillForm = function() {  $http({method: 'GET', url: callsConfig.getSettings})
            .success(function(data, status, headers, config) {
                $scope.SettingsForm = data;
            });
        };

        fillForm();
    }
]);

app.directive('adminGrid',[ function () {
    return {
        restrict: 'E',
        scope: {
            tblList: '=list',
            col1: '=firstColumn',
            col2: '=secondColumn',
            ctrl: '=passMethod'
        },
        templateUrl:  '../view/AdminGrid.tpl.html'
    }
}]);

app.directive('adminNews', function () {
    return {
        restrict: 'A',
        scope: {

            ctrl: '=methodNews'
        },
        templateUrl:  './view/AdminNews.tpl.html'
    }
});


app.directive('adminFiles', function () {
    return {
        restrict: 'A',
        scope: {
            ctrl: '=methodNews'
        },
        templateUrl:  './view/AdminFiles.tpl.html'
    }
});

app.directive('adminLinks', function () {
    return {
        restrict: 'A',
        scope: {
            ctrl: '=methodNews'
        },
        templateUrl:  './view/AdminLinks.tpl.html'
    }
});

app.directive('adminUsers', function () {
    return {
        restrict: 'A',
        scope: {
            ctrl: '=methodNews'
        },
        templateUrl:  './view/AdminUsers.tpl.html'
    }
});

app.directive('adminSettings', function () {
    return {
        restrict: 'A',
        scope: {
            ctrl: '=methodNews'
        },
        templateUrl:  './view/AdminSettings.tpl.html'
    }
});

app.directive('adminLogin', function () {
    return {
        restrict: 'E',
        scope: {

            ctrl: '=methodNews'
        },
        templateUrl:  '../view/AdminLogin.tpl.html'
    }
});

app.factory('callsConfig', function () {
   return (
       {
           //create, update, login & logout are on form submit

           getNews: '/api/news',
           getNewsById: '/api/news/',
           deleteNewsById: '/api/newsDelete/',

           getFiles: '/api/files',
           getFileById: '/api/files/',
           deleteFileById: '/api/fileDelete/',

           getLinks: '/api/links',
           getLinkById: '/api/links/',
           deleteLinkById: '/api/linkDelete/',

           getUsers: '/api/users',
           getUserById: '/api/users/',
           deleteUserById: '/api/userDelete/',

           getSettings: '/api/settings',

           hasAuth: '/api/hasAuth'

//           getRootLoc: '',
//           getNewsImgLoc: '',
//           getFilesLoc: '',
//           //
//
//           logIn: '',
//           logOff: ''
       }
   );

});




app.factory('loginSettings', function () {

    return {
        showLogin: function() {return true},
        hideLogin: function() {return false}
    //    logOff: function() {},
    //    logOn: function() {}
    };
});

