
(function(){

   // Utility function.

    var getIndex = function( aryObj, value) {
        return $.map(aryObj, function(obj, index) {
            if(obj.itemId == value) {
                return index;
            }
        });
    };

    var getItem = function (aryObj, itemId) {
           return getIndex(aryObj, itemId)[0];
    };

    var getParameterByName = function(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    };

    //if query parm passed, show that one and hide all others.
    var itemIdSelected = getParameterByName('item_id');


    /**
     *  Customization of Items of ItemTree via Item Id
     *
     *  Each var matches to and existing Item Id from the ItemTree
     *
     * These are positional across the page: MEM_1, MEM_2, MEM_3 represent Left, Middle, & Right respectively
     *
     * Change only the number as shown here:
     *
     *    var MEM_1_1YR = getIndex( items,  XXXX)[0];  where XXXX is the Item Id
     *
     */

    // Friend
    var MEM_1_1YR = getItem( items,  7849);   // <<-- only change Item Id for each, replace 0 with new Item Id for Membership Selected.
    var MEM_1_2YR = getItem( items,  7888);
    var MEM_1_RENEW = getItem( items, 8392);
    var MEM_1_GIFT_1YR = getItem( items, 2344);
    var MEM_1_GIFT_2YR = getItem( items, 3333);

    // Patron
    var MEM_2_1YR = getItem( items,  12313);
    var MEM_2_2YR = getItem( items,  12340);
    var MEM_2_RENEW = getItem( items, 12932);
    var MEM_2_GIFT_1YR = getItem( items, 15253);
    var MEM_2_GIFT_2YR = getItem( items, 15271);

    //Circle
    var MEM_3_1YR = getItem( items,  0);
    var MEM_3_2YR = getItem( items,  0);
    var MEM_3_RENEW = getItem( items, 0);
    var MEM_3_GIFT_1YR = getItem( items, 0);
    var MEM_3_GIFT_2YR = getItem( items, 0);

    // Fellow
    var MEM_4_1YR = getItem( items,  0);   // <<-- only change Item Id for each, replace 0 with new Item Id for Membership Selected.
    var MEM_4_2YR = getItem( items, 0);
    var MEM_4_RENEW = getItem( items, 0);
    var MEM_4_GIFT_1YR = getItem( items, 0);
    var MEM_4_GIFT_2YR = getItem( items, 0);

    // Sponsor
    var MEM_5_1YR = getItem( items,  0);
    var MEM_5_2YR = getItem( items,  0);
    var MEM_5_RENEW = getItem( items, 0);
    var MEM_5_GIFT_1YR = getItem( items, 0);
    var MEM_5_GIFT_2YR = getItem( items, 0);

    // Unused
    var MEM_6_1YR = getItem( items,  0);
    var MEM_6_2YR = getItem( items,  0);
    var MEM_6_RENEW = getItem( items, 0);
    var MEM_6_GIFT_1YR = getItem( items, 0);
    var MEM_6_GIFT_2YR = getItem( items, 0);


    /**
     * View Additional Levels link. Replace additional node_id to folder in Itemtree to goto that page. That folder should have and xslt assigned to it.
     *
     *
     */

    $("#levels").attr('href','ItemList.aspx?node_id=94870');   //  <<-- change 0 to new node_id from ItemTree of folder to go to different pages.

    /**
     *
     * End of Customization Section.
     *
     */




    <!--Main page changes-->
    $("title").html("Membership");
    $("#ILDataGrid").addClass('dataGridBlock');
    $("#ILCalendar").hide();
    $(".breadCrumbs").hide();
    $('#ILDataGrid').find('h3').hide();




    <!-- Assignments up to 6 membership positions -->
    if(MEM_1_1YR == undefined) {
        $("#memberBlock1").hide();
    }
    else {
        $("#membership1Price").html("" + items[MEM_1_1YR].priceStr.toString());
        $("#membership1Title").html(items[MEM_1_1YR].desc.toString());
        $("#membership1Text").html(items[MEM_1_1YR].helpInfo.toString());
        $("#membership1Yr1").attr('href', items[MEM_1_1YR].urlAbs);
        $("#membership1Yr2").attr('href', items[MEM_1_2YR].urlAbs);
        $("#membership1Renew").attr('href', items[MEM_1_RENEW].urlAbs);
        $("#membership1Gift_1").attr('href', items[MEM_1_GIFT_1YR].urlAbs);
        $("#membership1Gift_2").attr('href', items[MEM_1_GIFT_2YR].urlAbs);
    }

    if(MEM_2_1YR == undefined) {
        $("#memberBlock2").hide();
    }
    else {
        $("#membership2Price").html("" + items[MEM_2_1YR].priceStr.toString());
        $("#membership2Title").html(items[MEM_2_1YR].desc.toString());
        $("#membership2Text").html(items[MEM_2_1YR].helpInfo.toString());
        $("#membership2Yr1").attr('href', items[MEM_2_1YR].urlAbs);
        $("#membership2Yr2").attr('href', items[MEM_2_2YR].urlAbs);
        $("#membership2Renew").attr('href', items[MEM_2_RENEW].urlAbs);
        $("#membership2Gift_1").attr('href', items[MEM_2_GIFT_1YR].urlAbs);
        $("#membership2Gift_2").attr('href', items[MEM_2_GIFT_2YR].urlAbs);
    }

    if(MEM_3_1YR == undefined) {
        $("#memberBlock3").hide();
    }
    else {
        $("#membership3Price").html("" + items[MEM_3_1YR].priceStr.toString());
        $("#membership3Title").html(items[MEM_3_1YR].desc.toString());
        $("#membership3Text").html(items[MEM_3_1YR].helpInfo.toString());
        $("#membership3Yr1").attr('href', items[MEM_3_1YR].urlAbs);
        $("#membership3Yr2").attr('href', items[MEM_3_2YR].urlAbs);
        $("#membership3Renew").attr('href', items[MEM_3_RENEW].urlAbs);
        $("#membership3Gift_1").attr('href', items[MEM_3_GIFT_1YR].urlAbs);
        $("#membership3Gift_2").attr('href', items[MEM_3_GIFT_2YR].urlAbs);
    }

    if(MEM_4_1YR == undefined) {
        $("#memberBlock4").hide();
    }
    else {
        $("#membership4Price").html("" + items[MEM_4_1YR].priceStr.toString());
        $("#membership4Title").html(items[MEM_4_1YR].desc.toString());
        $("#membership4Text").html(items[MEM_4_1YR].helpInfo.toString());
        $("#membership4Yr1").attr('href', items[MEM_4_1YR].urlAbs);
        $("#membership4Yr2").attr('href', items[MEM_4_2YR].urlAbs);
        $("#membership4Renew").attr('href', items[MEM_4_RENEW].urlAbs);
        $("#membership4Gift_1").attr('href', items[MEM_4_GIFT_1YR].urlAbs);
        $("#membership4Gift_2").attr('href', items[MEM_4_GIFT_2YR].urlAbs);
    }

    if(MEM_5_1YR == undefined) {
        $("#memberBlock5").hide();
    }
    else {
        $("#membership5Price").html("" + items[MEM_5_1YR].priceStr.toString());
        $("#membership5Title").html(items[MEM_5_1YR].desc.toString());
        $("#membership5Text").html(items[MEM_5_1YR].helpInfo.toString());
        $("#membership5Yr1").attr('href', items[MEM_5_1YR].urlAbs);
        $("#membership5Yr2").attr('href', items[MEM_5_2YR].urlAbs);
        $("#membership5Renew").attr('href', items[MEM_5_RENEW].urlAbs);
        $("#membership5Gift_1").attr('href', items[MEM_5_GIFT_1YR].urlAbs);
        $("#membership5Gift_2").attr('href', items[MEM_5_GIFT_2YR].urlAbs);
    }

    if(MEM_6_1YR == undefined) {
        $("#memberBlock6").hide();
    }
    else {
        $("#membership6Price").html("" + items[MEM_6_1YR].priceStr.toString());
        $("#membership6Title").html(items[MEM_6_1YR].desc.toString());
        $("#membership6Text").html(items[MEM_6_1YR].helpInfo.toString());
        $("#membership6Yr1").attr('href', items[MEM_6_1YR].urlAbs);
        $("#membership6Yr2").attr('href', items[MEM_6_2YR].urlAbs);
        $("#membership6Renew").attr('href', items[MEM_6_RENEW].urlAbs);
        $("#membership6Gift_1").attr('href', items[MEM_6_GIFT_1YR].urlAbs);
        $("#membership6Gift_2").attr('href', items[MEM_6_GIFT_2YR].urlAbs);
    }


    // if query parm show only selected item.
    if (itemIdSelected == "66"){
        $("#memberBlock2").hide();$("#memberBlock3").hide();$("#memberBlock4").hide();$("#memberBlock5").hide();$("#memberBlock6").hide();
    } else if(itemIdSelected == "62") {
        $("#memberBlock1").hide();$("#memberBlock3").hide();$("#memberBlock4").hide();$("#memberBlock5").hide();$("#memberBlock6").hide();
    }else if(itemIdSelected == "63") {
        $("#memberBlock1").hide();$("#memberBlock2").hide();$("#memberBlock4").hide();$("#memberBlock5").hide();$("#memberBlock6").hide();
    }else if(itemIdSelected == "64") {
        $("#memberBlock1").hide();$("#memberBlock2").hide();$("#memberBlock3").hide();$("#memberBlock5").hide();$("#memberBlock6").hide();
    }else if(itemIdSelected == "65") {
        $("#memberBlock1").hide();$("#memberBlock2").hide();$("#memberBlock3").hide();$("#memberBlock4").hide();$("#memberBlock6").hide();
    }else if(itemIdSelected == "66") {
        $("#memberBlock1").hide();$("#memberBlock2").hide();$("#memberBlock3").hide();$("#memberBlock4").hide();$("#memberBlock5").hide();
    }


    <!--activate join toggle-->
    $('.join').on('click',function(){
        $('.gift').removeClass('inverseCartBtn');
        $('.join').removeClass('inverseCartBtn');
        $('.gift').next().hide();
        $('.join').next().hide();
        $(this).toggleClass('inverseCartBtn');
        $(this).next().toggle();
    });

    <!--activate gift toggle-->
    $('.gift').on('click',function(){
        $('.join').removeClass('inverseCartBtn');
        $('.gift').removeClass('inverseCartBtn');
        $('.join').next().hide();
        $('.gift').next().hide();
        $(this).toggleClass('inverseCartBtn');
        $(this).next().toggle();
    });


})();