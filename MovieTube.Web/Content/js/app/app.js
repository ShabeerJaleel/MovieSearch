
/// <reference path="../angular.js" />


(function () {

    var app = angular.module("movieApp", ['ngAnimate']);

    app.controller("MainController", function ($scope, $http, $log, $interval, $timeout) {

        (function () {

            var data = initService().bootData;
            $scope.thumbnails = data.thumbInfo.Thumbnails;

            $scope.settings = {
                activeView: 'latest',
                language: '',
                showWatchView: false,
                selectedYear: '',
                queryString: '',
                nextPage: data.thumbInfo.NextPage,
                showMoreLink: data.thumbInfo.NextPage > 0,
                showYear: true,
                playingIndex: -1,
                flashInstalled: FlashDetect.installed
            };


            //year list
            $scope.years = [];
            for (var i = 2014; i > 1971; i--)
                $scope.years.push(i);
            $scope.years.push(1960);
            $scope.years.push(1950);
            $scope.years.push(1920);
            $scope.years.push('Clear');

            var searchResult = {};

            //some jquery
            $('input.typeahead').typeahead(
            {
                source: function (query, process) {
                    $scope.queryString = query;
                    getData("/api/Query/Search/?term=" + query + "&language=" + $scope.settings.language)
                     .then(function (data) {
                         searchResult = data;
                         process(data);
                     });
                },
                afterSelect: function (query) {
                    $scope.settings.queryString = query.id;
                    $scope.Search(true);
                },
                autoSelect: false,
                showHintOnFocus: true
            });

        } ());

        //scope methods
        $scope.isActiveView = function (id) {
            return id === $scope.settings.activeView;
        }

        $scope.showMovieDetails= function (id) {
            return $scope.movie !== undefined;
        }

        $scope.activateThumbView = function (id, showYear, language) {

            setViewProperties(id, showYear, language);

            $timeout(function () {
                getThumbs();
            }, 10);
        }

        $scope.activateVideoView = function (event, thumbNail) {
            if (event)
                event.preventDefault();

            setViewProperties("watch", false, $scope.settings.language);

            if (thumbNail) {
               // $scope.movie = {};
                var url = "/api/Query/Movie/?id=" + thumbNail.Id;
                getData(url)
                    .then(function (data) {
                        $scope.movie = data;
                        $scope.play(0);
                    });

            } else {
                    $scope.play(0);
            }
        }

        $scope.loadMore = function (lang) {
            //load page
            $timeout(function () {
                getThumbs();
            }, 10);
        };

        $scope.highlightYear = function (year) {
            return year == $scope.settings.selectedYear;
        }

        $scope.selectYear = function (year) {
            if (year == $scope.settings.selectedYear)
                return;
            $scope.settings.selectedYear = year === 'Clear' ? '' : year;
            $scope.activateThumbView($scope.settings.activeView, true, $scope.settings.language);
        }

        $scope.Search = function (direct) {

            //if (!direct) {
            setViewProperties($scope.settings.language, $scope.settings.showYear, $scope.settings.language);
            //load page
            $timeout(function () {
                getThumbs($scope.settings.queryString);
            }, 10);
            //}
        };

        $scope.play = function ($index) {
            $scope.settings.playingIndex = $index;
            $scope.movie.Active = $scope.movie.Links[$index];
            angular.element(document.getElementById('player')).empty().append(
            '<object id="flashplayer" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="100%"' +
                        'height="100%" >' +
                        '<param name="movie" value="/Content/plugins/player.swf" />' +
                        '<param name="allowFullScreen" value="true" />' +
                        '<param name="allowScriptAccess" value="always" />' +
                        '<param name="autostart" value="true" />' +
                        '<param name="FlashVars" value="plugins=/Content/plugins/proxy.swf&proxy.link=' 
                        + $scope.movie.Active.Url + '&skin=/Content/plugins/modieus.zip" />' +
                        '<embed name="flashplayer" src="/Content/plugins/player.swf" type="application/x-shockwave-flash"' +
                         '   allowfullscreen="true" allowscriptaccess="always" width="100%" height="100%"' +
                          '  flashvars="plugins=/Content/plugins/proxy.swf&proxy.link=' 
                          + $scope.movie.Active.Url + '&skin=/Content/plugins/modieus.zip&autostart=true"' +
                           '  />' +
                    '</object>');
            $scope.settings.playingIndex = $index;
            //$scope.settings.playingIndex = -1;
            //$timeout(function () {
            // $scope.settings.playingIndex = $index;
            //}, 500);
            //player.jwPlaylistItem($index);
        }

        //end

        //private methods

        var setViewProperties = function (id, showYear, language) {
            //stop layout
            cancelTimer();

            //set values
            $scope.settings.nextPage = $scope.thumbnails.length = 0;
            $scope.settings.activeView = id;
            $scope.settings.showYear = showYear;
            $scope.settings.language = language;
            $scope.settings.showMoreLink = false;
            $scope.settings.playingIndex = -1;
        }

        var getThumbs = function (searchTerm) {

            var url = "/api/Query/List/?" + "language=" +
                      $scope.settings.language + "&year=" + $scope.settings.selectedYear +
                      "&page=" + $scope.settings.nextPage;
            if (searchTerm)
                url += "&term=" + searchTerm;

            getData(url)
            .then(function (data) {
                showThumbs(data);
            });
        };

        var showThumbs = function (data) {
            var currentIndex = 0;
            var thumbNails = data.Thumbnails;
            $scope.settings.nextPage = data.NextPage;
            cancelTimer();
            $scope.stop = $interval(function () {
                if (thumbNails.length === currentIndex) {
                    cancelTimer();
                    $scope.settings.showMoreLink = $scope.settings.nextPage > 0;
                }
                else
                    $scope.thumbnails.push(thumbNails[currentIndex++]);
            }, 1);
        };

        var cancelTimer = function () {
            if ($scope.stop !== undefined) {
                $interval.cancel($scope.stop);
                $scope.stop = undefined;
            }
        };

        var getData = function (url) {
            return $http.get(url)
                    .then(function (response) {
                        $log.log("data recieved");
                        return response.data;
                    });
        };
        //end

    });

    //Directives
    app.directive('errSrc', function () {
        return {
            link: function (scope, element, attrs) {

                scope.$watch(function () {
                    return attrs['ngSrc'];
                }, function (value) {
                    if (!value) {
                        element.attr('src', attrs.errSrc);
                    }
                });

                element.bind('error', function () {
                    element.attr('src', attrs.errSrc);
                });
            }
        }
    });
} ());