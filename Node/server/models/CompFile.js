var mongoose = require('mongoose');

var compFileSchema = mongoose.Schema({
    title: {type:String},
    location: {type: String},
    category: {type:String}
});

var CompFile = mongoose.model('CompFile', compFileSchema);

function createDefaultCompFiles() {
    CompFile.find({}).exec(function(err, collection){
        if(collection.length === 0){
            CompFile.create({title: 'Company Handbook', location: 'CompDoc.pdf', category:'General'});
            CompFile.create({title: 'Time Sheet', location: 'CompDoc.pdf', category:'General'});
            CompFile.create({title: 'Corporate Newsletter', location: 'CompDoc.pdf', category:'Marketing'});
            CompFile.create({title: 'Subscription Change Form', location: 'CompDoc.pdf', category:'Accounting'});
            CompFile.create({title: 'Logos', location: 'CompDoc.pdf', category:'Marketing'});
            CompFile.create({title: 'Sales Report', location: 'CompDoc.pdf', category:'Sales'});
        }

    })
}

exports.createDefaultCompFiles = createDefaultCompFiles;
