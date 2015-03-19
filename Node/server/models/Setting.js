var mongoose = require('mongoose');

var settingSchema = mongoose.Schema({
    name: {type:String},
    abbr: {type: String},
    image: {type: String},
    pageSize: {type: String},
    category:[
        {
            title: {type: String},
            selected:{type: String}
        }
    ]
});

var Setting = mongoose.model('Setting', settingSchema);

function createDefaultSettings() {
    Setting.find({}).exec(function(err, collection){
        if(collection.length === 0){
            Setting.create({name: 'Tango India Foxtrot', abbr: 'TIF', image: '../../public/img', pageSize: '5',
                category:
                    [
                        {title:'General', selected:''},
                        {title:'Sales', selected:''},
                        {title:'Marketing', selected:''},
                        {title:'Accounting', selected:''},
                        {title:'All', selected:''}
                    ]
             });

        }

    })
}

exports.createDefaultSettings = createDefaultSettings;
