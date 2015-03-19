var app = angular.module('Tif',['ngResource']);


app.controller('HomepageCtrl',['$scope', 'leftLinks', 'listFiles', 'compInfo', 'callsConfig', '$http',
    function($scope, leftLinks, listFiles, compInfo, callsConfig, $http) {

       // $scope.quickLinks = leftLinks;
       // $scope.listedFiles = listFiles;
        $scope.mainView = true;

        $scope.showDesc = 'Show Files ';
        $scope.companyInfo = compInfo;

        compInfo.$promise.then(function(data){
            $scope.menuItemList = data.category;
            $scope.fileFilter =  fileFilter($scope.menuItemList[0].title);
            $scope.newsPaging = {
                pageSize:  parseInt(data.pageSize),  // a default.
                prior: -1, // initial prev control set
                next:  parseInt(data.pageSize) //default.
            };
        });

        //for homepage use
        var fileFilter = function (selectedTitle) {
            return ({
                menu: $scope.menuItemList,
                selection: function(){
                    _( $scope.menuItemList).forEach(function (item) {
                        item.selected ='';
                    });
                    $scope.menuItemList[_( $scope.menuItemList).findIndex( {'title' : selectedTitle})].selected = 'active';
                    var tempTitle ='';
                    if(selectedTitle == 'All'){
                        tempTitle ='';// fix for all
                    } else
                    {   tempTitle = selectedTitle ; }
                    return  tempTitle;
                }
            });
        };

        // available functions
        this.toggleMainView= function (togValue) {
            if(togValue){
                $scope.mainView = false;
                $scope.showDesc = 'Show News ';
             }
            else {
                $scope.mainView = true;
                $scope.showDesc = 'Show Files ';
            }
        };

        this.setNews = function(newsId) {
            this.toggleMainView(false);
            ///
            $http({method: 'GET', url: callsConfig.getNewsById + newsId})
                .success(function (data, status, headers, config) {
                    $scope.centerNews = data;

                });
        }

        this.changeFileMenu = function(menuTitle){

            $scope.fileFilter = fileFilter(menuTitle);
        };

        this.priorNews = function (paging) {
            $http({method: 'GET', url: callsConfig.getNewsLinks + paging})
                .success(function(data, status, headers, config) {
                    $scope.newsItems = data;

                   $scope.newsPaging.prior = paging - $scope.newsPaging.pageSize;
                   if ($scope.newsPaging.prior < 0 ) {
                       $scope.newsPaging.prior = null;
                   }
                    else {
                       $scope.newsPaging.prior = paging - $scope.newsPaging.pageSize;
                   }
                    $scope.newsPaging.next = paging + $scope.newsPaging.pageSize;
                });
        };

        this.nextNews = function (paging) {
            $http({method: 'GET', url: callsConfig.getNewsLinks + paging})
                .success(function(data, status, headers, config) {
                    $scope.newsItems = data;

                    var dataCount = data.length;
                    if(dataCount < $scope.newsPaging.pageSize)
                        $scope.newsPaging.next = null;
                    else { // il sloppia ..fix
                            var tempPaging = (paging == 0) ? $scope.newsPaging.next : paging;
                            $http({method: 'GET', url: callsConfig.getNewsOneMore + (tempPaging)})
                                .success(function (data, status, headers, config) {
                                    if (data == 'true') {
                                        $scope.newsPaging.next = paging + $scope.newsPaging.pageSize;
                                    }
                                    else {
                                        $scope.newsPaging.next = null;
                                    }
                                })
                        }
                    if ($scope.newsPaging.prior < 0 )
                        $scope.newsPaging.prior = null;
                    else
                        $scope.newsPaging.prior = paging - $scope.newsPaging.pageSize;

                });
        };

        //..initial settings

        $scope.centerNews = this.setNews(0);
        $scope.newsItems = this.nextNews(0);
        return $scope.HomepageCtrl = this;

}]);

app.factory('compInfo', ['$resource', 'callsConfig', function ($resource,callsConfig) {
    var compInfo =  $resource(callsConfig.getSettings,  {
            get: {method:'GET', isArray:false}
         });
        return compInfo.get();
}]);

app.factory('callsConfig', function () {
    return (
        {
            getNewsPagingSize: '/api/newsPagingSize',
            getNews: '/api/news',
            getNewsLinks: '/api/newsLinks/',
            getNewsOneMore: '/api/newsMore/',
            getFiles: '/api/files',
            getLinks: '/api/links',
            getNewsById: '/api/news/',
            getSettings: '/api/settings'
        }
    );
});

app.factory('leftLinks', ['$resource', 'callsConfig',function ($resource,callsConfig) {
//        var sideLinks = $resource( callsConfig.getLinks,  {
//            get: {method:'GET', isArray:true}
//        });
//        return sideLinks.query();
}]);

app.factory('listFiles',['$resource', 'callsConfig', function ($resource, callsConfig) {
//    var listFiles = $resource( callsConfig.getFiles,  {
//        get: {method:'GET', isArray:true}
//    });
//    return listFiles.query();
}]);

app.directive('quickLink', function () {
    return {
        restrict: 'A',
        scope: {
            links: '=lists'
        },
        template:'<div ng-repeat="item in links" class="pad-bottom-5">' +
                 '<a href="{{item.url}}" target="_blank">{{item.title}}</a></div>'
    }
});
app.directive('quickLinkAlt', function () {
    return {
        restrict: 'A',
        scope: {
            links: '=lists'
        },
        template:'<span style="display:inline;" ng-repeat="item in links" class="pad-bottom-5">' +
            '<a href="{{item.url}}" target="_blank">{{item.title}}</a>&nbsp;&nbsp;</span>'
    }
});

app.directive('newsListing', function () {
    return {
        restrict: 'E',
        scope: {
           items: '=listItems',
          paging: '=paging',
            ctrl: '=methodNews'
        },
        templateUrl:  './view/MainNewsListing.tpl.html'
    }
});


app.directive('mainNews', function () {
    return {
        restrict: 'A',
        scope: {
           item: '=theNews',
           info: '=compInfo',
           ctrl: '=methodNews'
        },
        templateUrl: './view/MainNews.tpl.html'
    }
});

app.directive('mainFiles', function () {
    return {
        restrict: 'A',
        scope: {
            listing: '=currentListing',
          fItems: '=listFiles',
            info: '=compInfo',

            menu: '=menuItems',
            ctrl: '=methodMenu'
        },
        templateUrl: './view/MainFiles.tpl.html'
    }
});
