/**
 * defines
 */

    /**
     *  for calendar data
     * @type {Array}
     */
    var eventData =[];

    ///**
    // * Next 3 determines if item is to be shown.
    // * @param item -- current item on date
    // * @returns {boolean} true -- item shows.
    // */
    //
    //var isNotBlackOut = function (item, dateShort) {
    //
    //    var defaultDate = "1900-01-01T00:00:00";
    //
    //    if ((item.blackout1s == defaultDate) &&
    //        (item.blackout2s == defaultDate) &&
    //        (item.blackout3s == defaultDate) &&
    //        (item.blackout4s == defaultDate)) {
    //
    //        return true;
    //    }
    //    else {
    //        var resultBool = true;
    //        var currentDate = new Date(dateShort).setHours(0,0,0,0);
    //
    //
    //        if (item.blackout1s != defaultDate) {
    //           resultBool = resultBool && !((new Date(item.blackout1s).setHours(0,0,0,0) <= currentDate) && (currentDate <= new Date(item.blackout1e)));
    //        }
    //
    //        if (item.blackout2s != defaultDate) {
    //            resultBool = resultBool && !((new Date(item.blackout2s).setHours(0,0,0,0) <= currentDate) && (currentDate <= new Date(item.blackout2e).setHours(0,0,0,0)));
    //        }
    //        if (item.blackout3s != defaultDate) {
    //            resultBool = resultBool && !((new Date(item.blackout3s).setHours(0,0,0,0) <= currentDate) && (currentDate <= new Date(item.blackout3e).setHours(0,0,0,0)));
    //        }
    //        if (item.blackout4s != defaultDate) {
    //            resultBool = resultBool && !((new Date(item.blackout4s).setHours(0,0,0,0) <= currentDate) && (currentDate <= new Date(item.blackout4e).setHours(0,0,0,0)));
    //        }
    //
    //        return resultBool;
    //    }
    //
    //};
    //
    //
    //var isDOW = function (item, dateShort) {
    //    var d = new Date(dateShort);
    //    var itemDOW = d.getDay();
    //    var result ='';
    //
    //    // faster than eval()
    //    switch(itemDOW) {
    //
    //        case 0:
    //            result = item.show_mon;
    //            break;
    //        case 1:
    //            result = item.show_tue;
    //            break;
    //        case 2:
    //            result = item.show_wed;
    //            break;
    //        case 3:
    //            result = item.show_thu;
    //            break;
    //        case 4:
    //            result = item.show_fri;
    //            break;
    //        case 5:
    //            result = item.show_sat;
    //            break;
    //        case 6:
    //            result = item.show_sun;
    //            break;
    //        default:
    //            result ='true';
    //    }
    //
    //    return ( result.toLowerCase() === 'true');
    //};
    //
    //

    /**
    *
    * @param item
    * @param useNavAbsURL  bool: true - Use  NavigateUrlAbsolute DCI
    *                            false - static URL
    *                      default: false;
    */
    var useItemURL = function (item, useNavAbsURL) {

        if(useNavAbsURL == undefined || useNavAbsURL == null){ useNavAbsURL = false;}

        return (useNavAbsURL) ?  item.NavigateUrlAbsolute.toString() : 'ItemShow.aspx?Grp=VMMN/6m3F5Q=&Name=RIHikPWBeFgOAgdJBT8WiA==';

    };




    /**
     *  fetches and rebinds data to calendar.
     * @param selectDate  -- "" or less than today, set for today; forward data will be processed.
     *                       use js Date obj or ""
     */

    var renderCalendar = function(selectDate ) {

        if(selectDate ==="" || selectDate < new Date() ){ selectDate = new Date();}

        var startDate = selectDate;

        // Use this setting to set how many days of the calendar the call will query
        // fullCalendar needs to show up to 42 days or 6 weeks..refer to 8/2015
        var maxDate = moment(startDate).add(42, 'days').format("MM-DD-YYYY");
        startDate = moment(startDate).format("MM-DD-YYYY");

        var jsonD = 'api/ItemTree/GetItemTreeByDate?nodeId=' + window.parentNode + '&startDate=' + startDate + '&endDate=' + maxDate + '&quantity=1&recurse=0';

        $.ajax(jsonD, function (data) {

            }).done(function(data){

                var parsedJSON = [];

                /* loop through array */
                $.each(data, function (i, dayRoot) {

                    var dayItems = dayRoot.ItemList;
                    // console.log(ords);
                    $.each(dayItems, function (positionIndex, currentItem) {


                        var descrip = currentItem.descrip;

                        var Dates = currentItem.Availability;

                        //Use the URL setting to set the link value, it can either get it from the API call, or set statically
                        //var URL = ork.NavigateUrlAbsolute

                        var URL = useItemURL(currentItem, false);

                        var nDex = 1 + positionIndex;



                        if (Dates.length == 1 )
                        {
                            // single time on date
                            var itemOnDate =  Dates[0];

                            var dateLong = itemOnDate.AvailDate;

                            var dateShort = dateLong.substring(0, 10);

                            var title = "<span class=\'wrap" + nDex + "\'><span>$" + itemOnDate.Price.InitialPrice.toFixed(2) + "</span><span> - " + descrip + "</span><\/span>";

                            var itemData = {};
                            itemData["title"] = title;
                            itemData["start"] = dateShort + "T0" + nDex + ":00:00";
                            itemData["url"] = URL + "&d=" + dateShort;

                            if( itemOnDate.Available){
                                parsedJSON.push(itemData);
                            }


                        }
                        else {  // multiple times on Date

                            $.each(Dates, function (i, itemTimesOnDate) {

                                var dateLong = itemTimesOnDate.AvailDate;

                                var dateShort = dateLong.substring(0, 10);


                                var title = "<span class=\'wrap" + nDex +"\'><span>$" + itemTimesOnDate.Price.InitialPrice.toFixed(2) + "</span><span> - " + descrip + "</span><\/span>";

                                var itemData = {};
                                itemData["title"] = title;
                                itemData["start"] = dateShort + "T0" + nDex + ":00:00";
                                itemData["url"] = URL + "&d=" + dateShort;


                                if(!itemTimesOnDate.Available){
                                    parsedJSON.push(itemData);
                                }

                            });

                        }
                    });
                });
                eventData = parsedJSON;  // assign to global

                $("#calendar").fullCalendar('addEventSource', eventData);
                $('.fc-header-left').css('visibility','visible');   // btns back on, ready for clickers.

        });
    };




/**
 * Page load
 */

(function(){

    // expand grid width
    $("#ILDataGrid").removeClass('right').removeClass('TnB').removeClass('prcnt67').addClass('prcnt80');

    $('#ILDataGrid h3:first').html('Items Calendar');

    $('#ctl00_ContentPlaceHolder1_lbBackToTop').hide();
    //$('#ctl00_ContentPlaceHolder1_lbdate').hide();
    //$('#ctl00_ContentPlaceHolder1_PriceAvail').hide();
    $('#ILCalendar').hide();

    /** no need to prime data, method starts on today: renderCalendar()
        See last comment to start on future date
     **/

    // set fullCalendar defaults
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: ''
        },
        editable: false,
        lazyFetching: false,
        events: eventData,  //this loads items
        cache: false,
        dayRender: function (date, cell) {
            date = moment(date).format("MM-DD-YYYY");
            var today = new Date();
            today = moment(today).subtract(0, 'days').format("MM-DD-YYYY");
            if (date < today) {
                $(cell).addClass('disabled');  // style days prior to today
            }
        },
        eventRender: function (event, element) {
            element.find('span.fc-event-title').html(element.find('span.fc-event-title').text());
        },
        loading: function (bool) {
            $('#loading').toggle(bool);
        }
        ,
        viewRender: function (view, element) {

            eventData = [];  // clear prior data.

            $('.fc-header-left').css('visibility','hidden');  //hide btns to avoid anxious clickers...clear/refresh needs time.  [ class .fc-state-disabled not fast enough ]

            $("#calendar").fullCalendar('removeEvents');  // remove old data to allow rebinding of fresh data.

            renderCalendar(view.start);   // get data for cal month showing.
        }
    });


    /**
     * Use to start calendar on a future date other than today.
     *
     *  $('#calendar').fullCalendar( 'gotoDate', '12-1-2015' );
     */


})();



