var mongoose = require('mongoose');

var applicantSchema = mongoose.Schema({
    firstName: {type:String},
    lastName: {type:String},
    address: {type:String},
    city: {type:String},
    state: {type:String},
    zipCode: {type:String},
    email: {type:String},
    bestPhone: {type:String},
    bestTime: {type:String},
    altPhone: {type:String},
    resumeFile: {type:String},
    coverFile: {type:String},
    date: {type:Date},
    skills: [{skill : String, expLevel: Number, years: Number  }],
    educations: [{school : String, degree: String }],
    certifications: [{cert : String }],
    jobId: {type:String}
});

var Applicant = mongoose.model('Applicant', applicantSchema);

function createDefaultApplicant() {
    Applicant.find({}).exec(function(err, collection){
        if(collection.length === 0){
            Applicant.create(
                {
                    firstName: 'James',
                    lastName: 'Richmond Jr.',
                    address: '1231 Main St.',
                    city: 'Houston',
                    state: 'TX',
                    zipCode: '77001',
                    email: 'jrichmondjr@freeEmail.com',
                    bestPhone: '713-000-0000',
                    bestTime: '8am M-F',
                    altPhone: '713-000-0000',
                    resumeFile: '',
                    coverFile: '',
                    date: new Date('2014-8-21'),
                    skills: [{skill : 'Javascript', expLevel: 4, years: 3  },{skill : 'CSS', expLevel: 2, years: 2  }],
                    educations: [{school : 'Delgado', degree: 'BS' }],
                    certifications: [{cert : 'TTWM' }],
                    jobId: '4893845'
                }
            );
        }
    })
}

exports.createDefaultApplicant = createDefaultApplicant;
