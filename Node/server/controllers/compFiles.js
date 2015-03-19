var CompFile = require('mongoose').model('CompFile'),
    config = require('../../server/config/config') [process.env.NODE_ENV],
    fs = require('fs');


exports.getCompFiles = function(req,res) {
    CompFile.find({}).sort({title:1}).exec(function(err, collection) {
        res.send(collection);

    })
};


exports.getFileById = function(req, res) {
    CompFile.findOne({_id: req.params.id}).exec(function (err, item) {
        res.send(item);
    })
};

exports.deleteFileById = function(req, res) {
    CompFile.findOne({ _id: req.params.id }).remove().exec();
    res.status('ok').end();
};

// posts
exports.updateFile = function(req, res) {
    var body = req.body,
        hasImage = false;
        //nowDate = new Date();

    if(body.id == undefined)
    {res.redirect('../admin/#/Files');}

    if(req.files.fileImage != undefined){
        hasImage = true;
        fs.readFile(req.files.fileImage.path, function (err, data) {

            fs.createReadStream(req.files.fileImage.path)
                .pipe(fs.createWriteStream(config.rootPath + "/"+ config.siteRoot +"/public/docs/" + req.files.fileImage.originalname));
            fs.unlinkSync(req.files.fileImage.path);

        });
    }

    CompFile.findOne({ _id: body.id }, function (err, doc){
        doc.title = body.fileTitle;
        doc.category = body.fileCat;
        if (hasImage) {doc.location = req.files.fileImage.originalname}
        // doc.__v.$inc();
        doc.save();
    });

    res.redirect('../admin/#/Files');
};

exports.createFile= function(req, res) {
    var body = req.body,
        hasImage = false;
     //   nowDate = new Date();

    if(body.id != '000')
    {res.redirect('../admin/#/Files');}

    if(req.files.fileImage != undefined){
        hasImage = true;
        fs.readFile(req.files.fileImage.path, function (err, data) {

            fs.createReadStream(req.files.fileImage.path)
                .pipe(fs.createWriteStream(config.rootPath + "/"+ config.siteRoot +"/public/docs/" + req.files.fileImage.originalname));
            fs.unlinkSync(req.files.fileImage.path);
        });
    }

    var newDoc =  new CompFile(
        {
            'title' : body.fileTitle,
            'category' : body.fileCat,
            'location': (hasImage) ? req.files.fileImage.originalname : "blankfile.pdf"
        }
    );
    newDoc.save();
    res.redirect('../admin/#/Files');
};


