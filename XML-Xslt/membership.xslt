<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:template match="/">
       <!--define global-->
        <style>
            .inverseCartBtn {
                background-color: #000000 !important;
                color: #ffffff !important;
            }
            .cartBtns {
                display: inline-block;
                border: 1px solid #000;
                text-transform: uppercase;
                font-size: 12px;
                line-height: 12px;
                font-weight: normal;
                padding: 4px 4px 3px 4px;
                margin: 0 8px 8px 0;
            }
            .memberBlock {
                font-family: hg-t55, Helvetica, Arial, sans-serif;
                font-size: 14px;
                width: 400px;
                line-height: 16px;
                float: left;
            }
            .membershipText {
                color: #000;
                font-weight: bolder;
                font-size: 1.5em;
                line-height: 20px;
            }
            .membershipInfo {
                color: #999;
                font-weight: bolder;
                font-size: 1.5em;
                line-height: 20px;
            }
            .boxWide {
                width: 340px ;
            }
            .pullRight {
                padding-top:8px;
                top:0;
                right:0;
                width: 40%;
                position: absolute;
            }
            .membershipRow {
                width:100%;
            }
            #membership sub {
                color: #999;
            }
        </style>
        <script>
            var items = [];

        </script>

        <!--parse data-->
        <xsl:for-each select="ArrayOfItmListItem/ItmListItem">
            <script>
                (function(){
                var item = {};
                <!--define values for page-->
                    item.desc     = '<xsl:value-of select="descrip" disable-output-escaping="yes"/>';
                    item.priceStr = "$"+ parseFloat('<xsl:value-of select="Price" disable-output-escaping="no"/>').toFixed(0).toString();
                    item.priceDec = parseFloat('<xsl:value-of select="Price" disable-output-escaping="no"/>').toFixed(2);
                    item.nodeId   = '<xsl:value-of select="node_id" disable-output-escaping="yes"/>'
                    item.urlAbs   = '<xsl:value-of select="NavigateUrlAbsolute" disable-output-escaping="yes"/>';
                    item.item     = '<xsl:value-of select="item" disable-output-escaping="yes"/>';
                    item.itemId     = '<xsl:value-of select="item_id" disable-output-escaping="yes"/>';
                    item.helpInfo = '<xsl:value-of select="help_info" disable-output-escaping="yes"/>';
                items.push(item);
                })();
            </script>
        </xsl:for-each>


                <!--Layout start-->

        <!--<h1>MEMBERSHIP</h1>-->
        <h1>MEMBERSHIP</h1>

        <div class="displayIn introText">


            <div class="placeholderText">
                <h4>Core benefits</h4>
                <ul>
                    <li>Skip the line and visit free, anytime</li>
                    <li>Attend exclusive members-only events</li>
                    <li>Enjoy 10%-20% discounts at the restaurant, shop, and nearby retailers</li>
                    <li>Customize your benefits by selecting the series of your choice: Social, Insider, Learning, Family or Philanthropy.</li>
                </ul>
            </div>

            <div class="placeholderText">
                <h4>Founding benefits</h4>
                <ul>
                    <li>Founding member preview in April 2015, before the public opening</li>
                    <li>Special recognition and limited-edition founding membership cards</li>
                    <li>Presentations and special receptions with key staff</li>
                    <li>First year anniversary celebration in the new building</li>
                    <li>10% of your gift will be directed to support the new building</li>
                </ul>
            </div>
            <div class="renewBlock">
                <h2>RENEWING?<br/>ALREADY A MEMBER?</h2>
                <p>Login to view your membership information.</p>
                <a href="GuestLookup.aspx" class="button">LOGIN</a>
            </div>
        </div>
        <hr />




        <div class="displayIn">

<div class="membershipRow">
            <!--  Membership  1-->
            <div class="memberBlock" id="memberBlock1">
                <span id="membership1Title" class="membershipText">put something here or error</span><br/>
                <span id="membership1Price" class="membershipInfo">price</span><br/><br/>
                <span id="membership1Text" class="smallText">something here.</span><br/>
                <div class="btnAlign">

                    <a href="javascript:void(0)" id="membership1Join" class="mbrSelect cartBtns join">JOIN</a>

                    <div class="autoRenew boxWide">
                                <div class="">
                                    Join as a Founding member
                                    <br/>
                                    Support the new Building and
                                    <br/>
                                    get exclusive invites
                                    <br/><br/>
                                </div>
                                <div class="pushText pullRight">
                                    No thanks, join for
                                    <br/>
                                    one year<br/><br/><br/>
                                    <a id="membership1Yr1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                        1-YEAR
                                    </a>
                                </div>
                        <span class="btnAlign">
                            <a id="membership1Renew" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                AUTO-RENEW
                            </a>
                            <a id="membership1Yr2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>

                    <a href="javascript:void(0)" id="membership1Gift" class="mbrSelect cartBtns gift">GIFT</a>
                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership1Gift_1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership1Gift_2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>
                </div>
            </div>

            <!--  Membership  2-->
            <div class="memberBlock" id="memberBlock2">
                <span id="membership2Title" class="membershipText">put something here or error</span><br/>
                <span id="membership2Price" class="membershipInfo">price</span><br/><br/>
                <span id="membership2Text" class="smallText">One year of core benefits for two.</span><br/>
                <div class="btnAlign">

                    <a href="javascript:void(0)" id="membership2Join" class="mbrSelect cartBtns join">JOIN</a>

                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership2Yr1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership2Renew" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                AUTO-RENEW
                            </a>
                            <a id="membership2Yr2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>

                    <a href="javascript:void(0)" id="membership2Gift" class="mbrSelect cartBtns gift">GIFT</a>
                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership2Gift_1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership2Gift_2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>
                </div>
            </div>

            <!--  Membership  3-->
            <div class="memberBlock" id="memberBlock3">
                <span id="membership3Title" class="membershipText">put something here or error</span><br/>
                <span id="membership3Price" class="membershipInfo">price</span><br/><br/>
                <span id="membership3Text" class="smallText">One year of core benefits for two.</span><br/>
                <div class="btnAlign">

                    <a href="javascript:void(0)" id="membership3Join" class="mbrSelect cartBtns join">JOIN</a>

                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership3Yr1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership3Renew" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                AUTO-RENEW
                            </a>
                            <a id="membership3Yr2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>

                    <a href="javascript:void(0)" id="membership3Gift" class="mbrSelect cartBtns gift">GIFT</a>
                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership3Gift_1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership3Gift_2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>
                </div>
            </div>
   <hr/>  <!-- force row to full width-->
</div>

<div class="membershipRow">
            <!--  Membership  4-->
            <div class="memberBlock" id="memberBlock4">
                <span id="membership4Title" class="membershipText">put something here or error</span><br/>
                <span id="membership4Price" class="membershipInfo">price</span><br/><br/>
                <span id="membership4Text" class="smallText">One year of core benefits for two.</span><br/>
                <div class="btnAlign">

                    <a href="javascript:void(0)" id="membership4Join" class="mbrSelect cartBtns join">JOIN</a>

                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership4Yr1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership4Renew" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                AUTO-RENEW
                            </a>
                            <a id="membership4Yr2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>

                    <a href="javascript:void(0)" id="membership4Gift" class="mbrSelect cartBtns gift">GIFT</a>
                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership4Gift_1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership4Gift_2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>
                </div>
            </div>

            <!--  Membership  5-->
            <div class="memberBlock" id="memberBlock5">
                <span id="membership5Title" class="membershipText">put something here or error</span><br/>
                <span id="membership5Price" class="membershipInfo">price</span><br/><br/>
                <span id="membership5Text" class="smallText">One year of core benefits for two.</span><br/>
                <div class="btnAlign">

                    <a href="javascript:void(0)" id="membership5Join" class="mbrSelect cartBtns join">JOIN</a>

                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership5Yr1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership5Renew" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                AUTO-RENEW
                            </a>
                            <a id="membership5Yr2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>

                    <a href="javascript:void(0)" id="membership5Gift" class="mbrSelect cartBtns gift">GIFT</a>
                    <div class="autoRenew boxWide">
                        <div class="">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership5Gift_1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership5Gift_2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>
                </div>
            </div>

            <!--  Membership  6-->
            <div class="memberBlock" id="memberBlock6">
                <span id="membership6Title" class="membershipText">put something here or error</span><br/>
                <span id="membership6Price" class="membershipInfo">price</span><br/><br/>
                <span id="membership6Text" class="smallText">One year of core benefits for two.</span><br/>
                <div class="btnAlign">

                    <a href="javascript:void(0)" id="membership6Join" class="mbrSelect cartBtns join">JOIN</a>

                    <div class="autoRenew boxWide">
                        <div class="pushText">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership6Yr1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership6Renew" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                AUTO-RENEW
                            </a>
                            <a id="membership6Yr2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>

                    <a href="javascript:void(0)" id="membership6Gift" class="mbrSelect cartBtns gift">GIFT</a>
                    <div class="autoRenew boxWide">
                        <div class="pushText">
                            Join as a Founding member
                            <br/>
                            Support the new Building and
                            <br/>
                            get exclusive invites
                            <br/><br/>
                        </div>
                        <div class="pushText pullRight">
                            No thanks, join for
                            <br/>
                            one year<br/><br/><br/>
                            <a id="membership6Gift_1" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                1-YEAR
                            </a>
                        </div>
                        <span class="btnAlign">
                            <a id="membership6Gift_2" href="javascript:void(0)" class="mbrSelect cartBtns button">
                                2-YEARS
                            </a>
                        </span>
                    </div>
                </div>
            </div>

    <hr/>  <!-- force row to full width-->
</div>

    <div class="membershipRow">
        <div class="displayIn">
        <div id="linkBlock" class="memberBlock">
        <span id="linkHeading" class="membershipText">Additional Levels</span><br/>
        <!--<span class="membershipInfo">Text Placeholder </span>--><br/>
        <span id="linkTextPlaceholder" class="smallText">Text placeholder</span><br/><br/>
        <div class="btnAlign">
        <a href="#" id="levels" class="mbrSelect cartBtns">VIEW ADDITIONAL LEVELS</a>
        </div>
        </div>
        </div>
    </div>
</div>
        <!--Layout ends-->

        <!--js layout changes-->
        <div ><br/></div>
        <script src='Xslt/membership_xslt.js'></script>
    </xsl:template>

</xsl:stylesheet>

