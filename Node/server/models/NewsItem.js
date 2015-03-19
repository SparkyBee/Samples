var mongoose = require('mongoose');

var newsItemSchema = mongoose.Schema({
    title: {type:String},
    newsDate: {type: String},
    date: {type: Date},
    image: {type:String},
    active: {type:Boolean},
    positionRef: {type:String},
    person: {type:String},
    blurb: {type:String}
});

var NewsItem = mongoose.model('NewsItem', newsItemSchema);

function createDefaultNews() {
    NewsItem.find({}).exec(function(err, collection){
        if(collection.length === 0){
            NewsItem.create({title: 'DBA Administrator', newsDate: '8/21/2014', date:new Date('2014-8-21'),  image: '', active: true,
                positionRef: '#3923', person: 'Sally',
                blurb:'IPhone roof party pop-up, post-ironic small batch brunch wayfarers. Bicycle rights tousled Truffaut forage. Roof party farm-to-table small batch Cosby sweater Marfa.\r Kale chips messenger bag Intelligentsia tattooed, normcore lo-fi biodiesel wayfarers drinking vinegar letterpress pickled. Lo-fi typewriter narwhal, Tonx quinoa hoodie roof party street art leggings pickled. Tofu bespoke ethical vinyl. Cornhole chia hashtag vinyl beard vegan squid, ethical 90\'s chillwave cardigan tote bag.'+
                'Paleo freegan letterpress literally. Intelligentsia Marfa locavore skateboard ethical, gastropub ennui Pitchfork Wes Anderson roof party. Kickstarter Pinterest XOXO kogi, twee lomo mlkshk ethnic Marfa Thundercats shabby chic ennui.\r Austin sriracha craft beer biodiesel. Fingerstache normcore messenger bag organic 8-bit.\r Williamsburg fingerstache Pinterest, polaroid cardigan Kickstarter wolf PBR&B direct trade Truffaut meh kitsch locavore keffiyeh. Narwhal post-ironic Bushwick salvia.'
            });
            NewsItem.create({title: 'Administrator Assistant to Vice President', newsDate: '8/01/2014', date:new Date('2014-8-01'), image: '', active: true,
                positionRef: 'Enron Accounting', person: 'Jennifer',
                blurb:'Well, the way they make shows is, they make one show. That show\'s called a pilot. Then they show that show to the people who make shows, and on the strength of that one show they decide if they\'re going to make more shows. Some pilots get picked and become television programs. Some don\'t, become nothing. She starred in one of the ones that became nothing.'
            });
            NewsItem.create({title: 'Web Developer -- ASP.net, Javascript, CSS, SQL', newsDate: '7/05/2014', date:new Date('2014-7-05'), image: '', active: true,
                positionRef: 'BP Oil', person: 'Johnny',
                blurb:'Butcher skateboard flannel gluten-free, hoodie literally fixie polaroid deep v mumblecore mustache meggings ethnic trust fund. Craft beer irony Intelligentsia, occupy ethnic typewriter gentrify tofu.\r Vegan leggings roof party, gastropub polaroid XOXO occupy Shoreditch bicycle rights pickled lomo disrupt PBR. Viral lo-fi cray biodiesel, hoodie forage Kickstarter you probably haven\'t heard of them meh gastropub chillwave sustainable sartorial fanny pack chia.'
            });
            NewsItem.create({title: 'Sr. System Administrator -- Windows Server, Active Directory, Exchange', newsDate: '7/01/2014', date:new Date('2014-7-01'), image: '', active: true,
                positionRef: '#4485', person: 'Mortimer',
                blurb:'Objectively innovate empowered manufactured products whereas parallel platforms. Holisticly predominate extensible testing procedures for reliable supply chains. Dramatically engage top-line web services vis-a-vis cutting-edge deliverables.'+
                'Proactively envisioned multimedia based expertise and cross-media growth strategies. Seamlessly visualize quality intellectual capital without superior collaboration and idea-sharing. Holistically pontificate installed base portals after maintainable products.'
            });
            NewsItem.create({title: 'Director of IT -- Oil & Gas industries', newsDate: '5/01/2014', date:new Date('2014-5-01'), image: '',
                positionRef: '#723', person: 'Sally',
                blurb:'Okra gumbo Boudreaux turducken praline red beans & rice Thibideaux sac a lait. Turducken file praline Acadiana tasso bbq. Levee fishing levee okra gumbo jambalaya iced tea crawfish viens ci. Sac a lait lagniappe bayou ca c\'est bon sac a lait. Pirogue andouille make a roux turducken yams Fauxcheaux sa fait chaud Mardi Gras. Fried chicken fried catfish couillon okra gumbo smoked sausage viens ci Boudreaux remoulade.'+
                    'File Fauxcheaux fried chicken trail ride Mardi Gras tasso red beans & rice hot sauce beer. Sugar cane sauce piquante turducken macque choux cajun boudin po-boy King Cake pecan pie. Fried catfish gumbo sauce piquante trail ride Acadiana barbed wire Acadiana. Pirogue cher levee pirogue ca c\'est bon. Couillon interstate sugar cane red beans & rice envie boucherie etoufee. Praline tasso gumbo boudin fried chicken file bayou mirliton make a roux. Downtown zydeco crawfish gulf shrimp Lafayette bonjour beer sauce piquante.'
            });

        }

    })
}

exports.createDefaultNews = createDefaultNews;



