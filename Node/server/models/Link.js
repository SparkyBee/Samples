var mongoose = require('mongoose');

var linkSchema = mongoose.Schema({
    title: {type:String},
    url: {type: String}
});

var Link = mongoose.model('Link', linkSchema);

function createDefaultLinks() {
    Link.find({}).exec(function(err, collection){
        if(collection.length === 0){
            Link.create({title: 'Google', url: 'http://www.google.com'});
            Link.create({title: 'Modzilla', url: 'https://developer.mozilla.org/en-US/'});
            Link.create({title: 'U.S. News', url: 'http://news.google.com'});
        }

    })
}

exports.createDefaultLinks = createDefaultLinks;
