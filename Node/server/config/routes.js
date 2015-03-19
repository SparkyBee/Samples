var auth = require('./auth'),
	users = require('../controllers/users'),
  //  links = require('../controllers/links'),
    news = require('../controllers/newsItems'),
  //  compFiles = require('../controllers/compFiles'),
    settings = require('../controllers/settings'),
	mongoose = require('mongoose'),
    //jade = require('jade'),
	User = mongoose.model('User');


module.exports = function(app){

    //user
    app.post('/api/login', auth.authenticate);
    app.get('/api/hasAuth', auth.requiresLogin);
    app.get('/logout', function(req, res){
        req.logout();
        //res.end();
        res.redirect('/admin');
    });

    //if roles
    //	app.get('/api/users', auth.requiresRole('admin'), users.getUsers);
    //to secure api
    //  app.get('/api/links',auth.requiresApiLogin, links.getLinks);

    //fill tables & globals


    app.get('/api/news', news.getNewsItems);
    app.get('/api/newsMore/:skip', news.getNewsOneMore); //for next button
    app.get('/api/users', users.getUsers);
    app.get('/api/settings', settings.getSettings);
//  app.get('/api/links', links.getLinks);
//  app.get('/api/files', compFiles.getCompFiles);
    app.get('/api/newsLinks/:skip', news.getNewsLinks); //for links

    //news
    app.get('/api/news/:id', news.getNewsById);
    app.get('/api/newsDelete/:id', news.deleteNewsById);
    app.post('/api/updateNewsItem', news.updateNewsItem);
    app.post('/api/createNewsItem', news.createNewsItem);

    //files
//    app.get('/api/files/:id', compFiles.getFileById);
//    app.get('/api/fileDelete/:id', compFiles.deleteFileById);
//    app.post('/api/updateFile', compFiles.updateFile);
//    app.post('/api/createFile', compFiles.createFile);

    //links
//    app.get('/api/links/:id', links.getLinkById);
//    app.get('/api/linkDelete/:id', links.deleteLinkById);
//    app.post('/api/updateLink', links.updateLink);
//    app.post('/api/createLink', links.createLink);

    //users
    app.get('/api/users/:id', users.getUserById);
    app.get('/api/userDelete/:id', users.deleteUserById);
    app.post('/api/updateUser', users.updateUser);
    app.post('/api/createUser', users.createUser);

    //settings
    app.post('/api/updateSettings', settings.updateSettings);

    //nope
	app.all('/api/*', function(req,res){
		res.status(404).end();
	});

};