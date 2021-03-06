var express = require('express');

var env = process.env.NODE_ENV = process.env.NODE_ENV || 'production'; // development production

var app = express();

var config = require('./config/config') [env];

require('./config/express') (app, config);

require('./config/mongoose') (config);

require('./config/passport') ();

require('./config/routes') (app);

app.listen(config.port);

console.log(' == Listening on port '+ config.port + '...');
console.log(' == run as: ' + env + ' | __dirname: ' + __dirname +' | rootPath: '+ config.rootPath + ' | sitePath: '+ config.siteRoot);