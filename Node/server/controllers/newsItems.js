var NewsItem = require('mongoose').model('NewsItem'),
    setting = require('mongoose').model('Setting'),
    config = require('../../server/config/config') [process.env.NODE_ENV],
    fs = require('fs');

//use in saving
var stringDate = function (nowDate) {
      return  (nowDate.getMonth()+1) +"/" +  nowDate.getDate() +"/" +  nowDate.getFullYear();
};

// gets
exports.getNewsItems = function(req,res) {
    NewsItem.find({}).exec(function(err, collection) {
        res.send(collection);

    })
};

exports.getNewsLinks = function(req, res) {

    setting.findOne({}).exec(function(err, data) {
       var pageSize = (isNaN(data.pageSize)) ? config.defaultPaging : parseInt(data.pageSize);
        NewsItem.find({},{ blurb:0}).sort({date:-1}).skip(req.params.skip).limit(pageSize).exec(function (err, collection) {
            res.send(collection);
        })
    });
};

//verify if more exists
exports.getNewsOneMore = function(req, res) {
    NewsItem.find({},{_id:1}).sort({date:-1}).skip(req.params.skip).limit(1).exec(function (err, collection) {
        if (collection.length == 0)
        {
            res.send(false);
        }
        else{
            res.send(true);
        }
    })
};

exports.getNewsById = function(req, res) {
    if(req.params.id == 0){
        NewsItem.findOne().sort({date:-1}).exec(function (err, item) {
            res.send(item);
        })
    }
    else {
        NewsItem.findOne({_id: req.params.id}).exec(function (err, item) {
            res.send(item);
        })
    }
};

exports.deleteNewsById = function(req, res) {
    NewsItem.findOne({ _id: req.params.id }).remove().exec();
    res.status('ok').end();
};

// posts
exports.updateNewsItem = function(req, res) {
   var body = req.body,
       hasImage = false,
       nowDate = new Date();

   if(body.id == undefined)
        {res.redirect('../admin/#/News');}

   if(req.files.newsImage != undefined){
        hasImage = true;
        fs.readFile(req.files.newsImage.path, function (err, data) {

            fs.createReadStream(req.files.newsImage.path)
                .pipe(fs.createWriteStream(config.rootPath + "/"+ config.siteRoot +"/public/img/news/" + req.files.newsImage.originalname));
            fs.unlinkSync(req.files.newsImage.path);

        });
   }

   NewsItem.findOne({ _id: body.id }, function (err, doc){
        doc.title = body.newsTitle;
        doc.newsDate = stringDate(nowDate);
        doc.date = nowDate;
        doc.blurb = body.newsArticle;
        if(hasImage){
        doc.image = req.files.newsImage.originalname ;  //if nothing, don't replace
        }
        doc.save();
   });

   res.redirect('../admin/#/News');
};

exports.createNewsItem = function(req, res) {
    var body = req.body,
        hasImage = false,
        nowDate = new Date();

    if(body.id != '000')
    {res.redirect('../admin/#/News');}

    if(req.files.newsImage != undefined){
        hasImage = true;
        fs.readFile(req.files.newsImage.path, function (err, data) {

            fs.createReadStream(req.files.newsImage.path)
                .pipe(fs.createWriteStream(config.rootPath + "/"+ config.siteRoot +"/public/img/news/" + req.files.newsImage.originalname));
            fs.unlinkSync(req.files.newsImage.path);
        });
    }

   var newDoc =  new NewsItem(
            {
            'title' : body.newsTitle,
            'newsDate' : stringDate(nowDate),
            'date': nowDate,
            'image': (hasImage) ? req.files.newsImage.originalname : 'blank.png',
            'blurb': body.newsArticle
            }
        );
    newDoc.save();
    res.redirect('../admin/#/News');
};



