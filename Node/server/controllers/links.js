var Link = require('mongoose').model('Link');


exports.getLinks = function(req,res) {
    Link.find({}).exec(function(err, collection) {
        res.send(collection);
    })
};


exports.getLinkById = function(req, res) {
    Link.findOne({_id: req.params.id}).exec(function (err, item) {
        res.send(item);
    })
};

exports.deleteLinkById = function(req, res) {
    Link.findOne({ _id: req.params.id }).remove().exec();
    res.status('ok').end();
};

// posts
exports.updateLink = function(req, res) {
    var body = req.body;

    if(body.id == undefined)
    {res.redirect('../admin/#/Links');}

    Link.findOne({ _id: body.id }, function (err, doc){
        doc.title = body.linkTitle;
        doc.url = body.linkURL;
        doc.save();
    });

    res.redirect('../admin/#/Links');
};

exports.createLink= function(req, res) {
    var body = req.body;

//    if(body.id != '000')
//    {res.redirect('../admin/#/Links');next();}

    var newDoc =  new Link(
        {
            'title' : body.linkTitle,
            'url' : body.linkURL
        }
    );
    newDoc.save();
    res.redirect('../admin/#/Links');
};


