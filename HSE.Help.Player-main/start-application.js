const path = require('path');
const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
const url = require('url');

const api = require('./api/server');
const PORT = process.env.PORT || 2093;

const app = express();

// enable cors
app.use(cors());
// parse application/json
app.use(bodyParser.json());
// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }));

app.use('/themes', express.static(path.join(__dirname, 'themes')));
app.use('/React/dist', express.static(path.join(__dirname, 'dist')));
app.use('/api', api);

const adminIndexHTML = path.resolve(__dirname, 'dist/index.html');
app.get('/music', (req, res) => res.sendFile(adminIndexHTML));
app.get('/redirect', (req, res) => res.sendFile(adminIndexHTML));
app.get('/',  function(req, res) {
    res.redirect('/music#home');
});
app.listen(PORT, () => console.info('Server is running on port:', PORT));