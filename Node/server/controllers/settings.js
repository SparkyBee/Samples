var Setting = require('mongoose').model('Setting'),
    _ = require('lodash');


exports.getSettings = function(req,res) {
    Setting.findOne({}).exec(function(err, data) {
        res.send(data);
    })
};


exports.updateSettings = function(req,res) {
    Setting.findOne({}).exec(function(err, data) {

        data.name = req.body.compName;
        data.abbr = req.body.compAbbr;
        data.pageSize = req.body.page.toString();

        var cat = _.uniq([ req.body.cat1, req.body.cat2, req.body.cat3, req.body.cat4, req.body.cat5]);
        var categories =[];
        _.forEach(cat, function (val) {
              categories.push({title:val, selected:''});
        });

        data.category = categories;
        data.save();
        res.redirect('../admin/#/Settings');
    })
};
